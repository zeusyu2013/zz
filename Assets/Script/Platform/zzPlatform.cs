using UnityEngine;
using System.Collections;
using LitJson;

public class zzPlatform
{
    /// <summary>
    /// 设置接收消息对象名
    /// </summary>
    /// <param name="gameObjectName"></param>
    public static void zzPlatform_SetReceiveGameObjectName(string gameObjectName)
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        zzIos.zzIos_SetReceiveGameObjectName(gameObjectName);
#elif UNITY_ANDROID && !UNITY_EDITOR
        zzAndroid.Instance.zzAndroid_SetReceiveGameObjectName(gameObjectName);
#endif
    }

    /// <summary>
    /// 日志输出
    /// </summary>
    /// <param name="log"></param>
    public static void zzPlatform_Log(string log)
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        zzIos.zzIos_Log(log);   
#elif UNITY_ANDROID && !UNITY_EDITOR
      zzAndroid.Instance.zzAndroid_Log(log);  
#endif
    }


    #region <>获取渠道名<>

    public delegate void PlatformName(string result);
    public static PlatformName mPlatformName;

    /// <summary>
    /// 获取渠道名
    /// </summary>
    public static void GetPlatformName(PlatformName callback)
    {
#if UNITY_IPHONE && !UNITY_EDITOR
        zzIos.GetPlatformName(callback);
#elif UNITY_ANDROID && !UNITY_EDITOR
        zzAndroid.Instance.zzAndroid_Log(log);
#else
        JsonData data = new JsonData();
        data["platform"] = "zz";
        data["package"] = "com.zeusyu.zz";
        if (callback != null)
        {
            callback(data.ToJson());
        }
#endif

    }
    
    #endregion
}