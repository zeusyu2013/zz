using UnityEngine;
using System.Collections;

public class zzAndroid : MonoBehaviour
{
    private static zzAndroid _Instance = null;
    public static zzAndroid Instance
    {
        private set { }
        get
        {
            return _Instance;
        }
    }

    void Awake()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 返回Android渠道名
    /// </summary>
    public delegate void zzGetAndroidPlatformChannelName();
    public zzGetAndroidPlatformChannelName m_zzGetAndroidPlatformChannelName;

#if UNITY_ANDROID && !UNITY_EDITOR
    private static AndroidJavaClass mAndroidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
    private static AndroidJavaObject mAndroidJavaObject = mAndroidJavaClass.GetStatic<AndroidJavaObject>("currentActivity");

    /// <summary>
    /// 设置接收消息对象名
    /// </summary>
    /// <param name="gameObjectName"></param>
    public void zzAndroid_SetReceiveGameObjectName(string gameObjectName)
    {
        mAndroidJavaObject.CallStatic("SetReceiveGameObjectName", new object[] { gameObjectName });
    }

    /// <summary>
    /// Android日志
    /// </summary>
    /// <param name="log"></param>
    public void zzAndroid_Log(string log)
    {
        mAndroidJavaObject.CallStatic("Log", new object[] { log });
    }
#endif
}