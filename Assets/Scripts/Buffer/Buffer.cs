using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buffer : MonoBehaviour
{
    /// <summary>
    /// Buff���ص�����
    /// </summary>
    public abstract void OnBuffAdd(Attribute caster,Attribute parent);
    /// <summary>
    /// Buffִ��
    /// </summary>
    public abstract void OnBuffExecute(Attribute caster, Attribute parent);
    /// <summary>
    /// Buff�Ƴ�
    /// </summary>
    public abstract void OnBuffRemove(Attribute caster, Attribute parent);
}
