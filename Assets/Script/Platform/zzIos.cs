using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using LitJson;

public class zzIos : MonoBehaviour 
{
    [DllImport("__Internal")]
    public static extern void zzIos_SetReceiveGameObjectName(string gameObjectName);

    [DllImport("__Internal")]
    public static extern void zzIos_Log(string log);

    [DllImport("__Internal")]
    public static extern void zzIos_GetPlatformName();

    private static zzIos _Instance = null;
    public static zzIos Instance
    {
        private set { }
        get { return _Instance; }
    }

    void Awake()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);

        zzIos_SetReceiveGameObjectName("zzIos");
    }

    #region <>返回渠道名<>

    public delegate void PlatformName(string result);
    public static PlatformName mPlatformName;

    /// <summary>
    /// 获取渠道名
    /// </summary>
    public static void GetPlatformName(PlatformName callback)
    {
        zzIos_GetPlatformName();

        if (callback != null)
        {
            mPlatformName = callback;
        }
    }

    /// <summary>
    /// 返回渠道名
    /// </summary>
    /// <param name="result"></param>
    public void zzIos_PlatformName(string result)
    {
        if (mPlatformName != null)
        {
            mPlatformName(result);
        }
    }

    #endregion
}