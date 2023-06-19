using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpModifier : Modifier
{
    public override void OnBuffExecute(Attribute caster, Attribute parent)
    {
        if (caster != null && parent != null)
        {
            parent.SetAttributeModifyByType(AttributeType.Hp, -caster.GetAttributeByType(AttributeType.Attack));
        }
    }
}
