using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public LayerMask enemyLayer;

    private HpModifier hpModifier;
    private MotionModifier motionModifier;
    private EnemyMove enemyMove;
    public Attribute caster;

    private void Awake()
    {
        hpModifier = GetComponent<HpModifier>();
        motionModifier = GetComponent<MotionModifier>();
        enemyMove = GetComponent<EnemyMove>();
        caster = GetComponent<Attribute>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null)
        {
            if (((1 << other.gameObject.layer) | enemyLayer) == enemyLayer)
            {
                //Hit(other.gameObject);
                Vector2 offset = other.transform.position - transform.position;
                Attribute parent = other.gameObject.GetComponent<Attribute>();
                motionModifier.SetMotionDir(offset.normalized);
                motionModifier.OnBuffExecute(caster, parent);
            }
        }
    }

    private void Update()
    {
        if (enemyMove != null && enemyMove.onTouchEnemy && PlayerInputHandle.attribute!=null)//仅适用于单个角色
        {
            Hit(PlayerInputHandle.attribute.gameObject);
        }
    }

    private void Hit(GameObject other)
    {
        if (caster != null)
        {
            Attribute parent = other.GetComponent<Attribute>();
            if (parent == null)
            {
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
}
