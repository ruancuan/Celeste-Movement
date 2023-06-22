using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask groundLayer;
    public LayerMask enemyLayer;

    public float moveSpeed = 10f;
    private HpModifier hpModifier;
    public Attribute caster;

    private void Awake()
    {
        hpModifier = GetComponent<HpModifier>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, moveSpeed * Time.deltaTime);
    }

    private float timeToDisable = 10;
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
            }
        }
    }
    private void Hit(Collider2D other) {
        if (hpModifier != null && caster!=null) {
            Attribute parent = other.GetComponent<Attribute>();
            if (parent != null)
            {
                hpModifier.OnBuffExecute(caster, parent);
            }
        }
    }
    private void Recovery()
    {
        caster = null;
        GameObjectPool.Instance.PushOneBullet(this);
    }
}
