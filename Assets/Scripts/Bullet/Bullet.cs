using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    public float moveSpeed = 10f;
    public float timeToDisable = 10;
    private HpModifier hpModifier;
    private MotionModifier motionModifier;
    public Attribute caster;

    private void Awake()
    {
        hpModifier = GetComponent<HpModifier>();
        motionModifier = GetComponent<MotionModifier>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, moveSpeed * Time.deltaTime);
    }

    private void OnEnable()
    {
        Invoke("DisableSelf", this.timeToDisable);
    }

    private void DisableSelf() {
        if (this.enabled) {
            this.Recovery();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null) {
            if (((1 << other.gameObject.layer) | groundLayer) == groundLayer) {
                Recovery();
            }
            else if (((1 << other.gameObject.layer) | enemyLayer) == enemyLayer)
            {
                Hit(other);
                Recovery();
            }
        }
    }
    private void Hit(Collider2D other) {
        if (caster != null)
        {
            Attribute parent = other.GetComponent<Attribute>();
            if (parent == null) {
                return;
            }
            if (hpModifier != null)
            {
                if (parent != null)
                {
                    hpModifier.OnBuffExecute(caster, parent);

                }
            }
            if (motionModifier != null)
            {
                Vector2 offset = other.transform.position - transform.position;
                motionModifier.SetMotionDir(offset.normalized);
                motionModifier.OnBuffExecute(caster, parent);
            }
        }
    }
    private void Recovery()
    {
        caster = null;
        CancelInvoke("DisableSelf");
        GameObjectPool.Instance.PushOneBullet(this);
    }
}
