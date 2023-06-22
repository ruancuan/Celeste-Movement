using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogManager : MonoSingleton<LogManager>
{
    public void Log(string content) {
#if UNITY_EDITOR
        Debug.Log(content);
#endif
    }

    public void LogWarn(string content) {
#if UNITY_EDITOR
        Debug.LogWarning(content);
#endif
    }

    public void LogError(string content) {
#if UNITY_EDITOR
        Debug.LogError(content);
#endif
    }

}
