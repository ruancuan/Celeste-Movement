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
    private Dictionary<AttributeType, Action> onAttributeChangeDict=new Dictionary<AttributeType, Action>();
    /// <summary>
    /// 记录上一次受到力的时间，防止短时间内受到多次力的施加
    /// </summary>
    public float lastForceTime = 0;
    public void AddAttributeListener(AttributeType type, Action handle) {
        Action action;
        onAttributeChangeDict.TryGetValue(type, out action);
        if (action == null)
        {
            action += handle;
            onAttributeChangeDict.Add(type, action);
        }
        else {
            action += handle;
        }
    }
    public void RemoveAttributeListener(AttributeType type, Action handle)
    {
        Action action;
        onAttributeChangeDict.TryGetValue(type, out action);
        if (action != null)
        {
            action -= handle;
        }
    }
    public void TriggerAttributeChange(AttributeType type) {
        Action action;
        onAttributeChangeDict.TryGetValue(type, out action);
        if (action != null)
        {
            action.Invoke();
        }
    }

    public void SetAttributeModifyByType(AttributeType type,float modify) {
        for (int k = 0; k < attrList.Count; k++) {
            AttributeData data = attrList[k];
            if (data.type == type)
            {
                data.Current+=modify;
                attrList[k] = data;
                TriggerAttributeChange(type);
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
