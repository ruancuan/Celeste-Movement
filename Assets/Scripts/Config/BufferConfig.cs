using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType { 
    Hp,
    Motion
}
[CreateAssetMenu(fileName = "Buffer", menuName = "Buffe/AddBuff")]
public class BufferConfig : ScriptableObject
{
    public BuffType buffType;
    public float hpModify;
    public float positionModify;
}
