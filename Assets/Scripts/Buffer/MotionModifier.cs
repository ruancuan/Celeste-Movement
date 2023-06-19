using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionModifier : Modifier
{
    public Vector3 dir = Vector3.zero;
    public float offset = 1f;
    public override void OnBuffExecute(Attribute caster, Attribute parent)
    {
        if (parent) {
            Rigidbody2D rig = parent.GetComponent<Rigidbody2D>();
            if (rig) {
                rig.AddForce(dir);
            }
        }
    }
}
