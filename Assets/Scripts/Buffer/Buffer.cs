using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buffer : MonoBehaviour
{
    /// <summary>
    /// Buff挂载到身上
    /// </summary>
    public abstract void OnBuffAdd(Attribute caster,Attribute parent);
    /// <summary>
    /// Buff执行
    /// </summary>
    public abstract void OnBuffExecute(Attribute caster, Attribute parent);
    /// <summary>
    /// Buff移除
    /// </summary>
    public abstract void OnBuffRemove(Attribute caster, Attribute parent);
}
