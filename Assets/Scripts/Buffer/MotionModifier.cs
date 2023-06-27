using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionModifier : Modifier
{
    public Vector2 dir = Vector2.zero;
    [Header("击退力度")]
    public float forceScale = 10f;
    public float forceAddInterval = 0.05f;

    public void SetMotionDir(Vector2 dir) {
        this.dir = dir;
    }
    public override void OnBuffExecute(Attribute caster, Attribute parent)
    {
        if (parent) {
            Rigidbody2D rig = parent.GetComponent<Rigidbody2D>();
            if (rig) {
                //限制最大的力度
                if (Time.realtimeSinceStartup-parent.lastForceTime > forceAddInterval)
                {
                    rig.AddForce(dir * forceScale);
                    parent.lastForceTime = Time.realtimeSinceStartup;
                }
            }
        }
    }
}
