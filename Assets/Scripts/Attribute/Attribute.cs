using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum AttributeType { 
    Hp,
    Attack
}

[Serializable]
public struct AttributeData {
    public AttributeType type;
    public float min;
    public float max;
    [SerializeField]
    private float current;
    public float Current
    {
        get {
            return current;
        }
        set {
            current = value;
            current = Mathf.Clamp(current, min, max);
        }
    }
    public AttributeData(AttributeType t, float min, float max) {
        this.type = t;
        this.min = min;
        this.max = max;
        this.current = max;
    }
}
public class Attribute : MonoBehaviour
{
    public List<AttributeData> attrList=new List<AttributeData>();
    public void SetAttributeModifyByType(AttributeType type,float modify) {
        for (int k = 0; k < attrList.Count; k++) {
            AttributeData data = attrList[k];
            if (data.type == type)
            {
                data.Current+=modify;
                break;
            }
        }
    }

    public float GetAttributeByType(AttributeType type)
    {
        for (int k = 0; k < attrList.Count; k++)
        {
            AttributeData data = attrList[k];
            if (data.type == type)
            {
                return data.Current;
            }
        }
        return 0;
    }
}
