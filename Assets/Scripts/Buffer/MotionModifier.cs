using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionModifier : Modifier
{
    public Vector2 dir = Vector2.zero;
    [Header("»÷ÍËÁ¦¶È")]
    public float forceScale = 10f;

    public void SetMotionDir(Vector2 dir) {
        this.dir = dir;
    }
    public override void OnBuffExecute(Attribute caster, Attribute parent)
    {
        if (parent) {
            Rigidbody2D rig = parent.GetComponent<Rigidbody2D>();
            if (rig) {
                rig.AddForce(dir* forceScale);
            }
        }
    }
}
