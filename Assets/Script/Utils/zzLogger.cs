using UnityEngine;
using System.Collections;

public class zzLogger
{
    public static void Log(string log)
    {
        if (!Debug.isDebugBuild)
        {
            return;
        }
#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR
        zzPlatform.zzPlatform_Log(log);
#else
        Debug.Log(log);
#endif
    }

    public static void LogWarrning(string log)
    {
        if (!Debug.isDebugBuild)
        {
            return;
        }

#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR

#else
        Debug.LogWarning(log);
#endif
    }

    public static void LogError(string log)
    {
        if (!Debug.isDebugBuild)
        {
            return;
        }

#if (UNITY_IPHONE || UNITY_ANDROID) && !UNITY_EDITOR

#else
        Debug.LogError(log);
#endif
    }
}