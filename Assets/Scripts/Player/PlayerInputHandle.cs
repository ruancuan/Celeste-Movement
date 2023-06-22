using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandle : MonoBehaviour
{
    public Transform cameraFollow;
    public float offset=2f;
    public static KeyCode shootKey = KeyCode.Mouse1;
    public IWeapon weapon;
    private Vector3 leftPosition;
    private Vector3 rightPosition;
    private Attribute attribute;
    private int corpseLayer;
    private float timeToDisable = 10;
    private Movement movement;
    private Animator animatior;

    //private 

        // Start is called before the first frame update
    void Start()
    {
        weapon = GetComponent<R99>();
        leftPosition = new Vector3(-offset, -2, 0);
        rightPosition = new Vector3(offset, -2, 0);
        attribute = GetComponent<Attribute>();
        movement = GetComponent<Movement>();
        animatior = GetComponent<Animator>();
        corpseLayer = LayerMask.NameToLayer("Corpse");
        InitEventListener();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(shootKey)) {
            UseWeapon();
        }
        float xRaw = Input.GetAxisRaw("Horizontal");
        if (xRaw != 0 && weapon!=null) {
            weapon.SetDir(xRaw, 0);
            if (cameraFollow != null) {
                if (xRaw > 0)
                {
                    cameraFollow.transform.localPosition = rightPosition;    
                }
                else
                {
                    cameraFollow.transform.localPosition = leftPosition;
                }
            }
        }
    }

    private void UseWeapon() {
        if (weapon!=null) {
            weapon.Execute();
        }
    }


    private void InitEventListener()
    {
        if (attribute != null)
        {
            attribute.AddAttributeListener(AttributeType.Hp, OnEnemyHit);
        }
    }

    private float lastHitTime = 0;
    private float hitInterval = 1f;
    private void OnEnemyHit()
    {
        if (Time.realtimeSinceStartup - lastHitTime >= this.hitInterval)
        {
            lastHitTime = Time.realtimeSinceStartup;
            AudioManager.Instance.PlayHitAudio();

            if (attribute.GetAttributeByType(AttributeType.Hp) <= 0)
            {
                this.DeadHandle();
            }

        }
    }

    private void DeadHandle()
    {
        this.gameObject.layer = corpseLayer;
        this.enabled = false;
        this.movement.enabled = false;
        Time.timeScale = 0.1f;
        //animatior.enabled = false;
    }

}
