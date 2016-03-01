using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class GameMain : MonoBehaviour 
{
    /*
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

#if UNITY_ANDROID && !UNITY_EDITOR
        zzCommonUtils.AddComponent("zzAndroid");
#elif UNITY_IPHONE && !UNITY_EDITOR
        zzCommonUtils.AddComponent("zzIos");
#endif
    }

    void Start()
    {
        // 1.检查渠道
        zzPlatform.GetPlatformName(GetPlatform);
    }
    */

    void Start()
    {
        ObjectManager.Instance.Clear();

        LoadingUIPanel panel = ViewManager.Instance.OpenUI<LoadingUIPanel>();
        panel.Init();

        ADManager.Instance.ShowRewardedAd();
    }

    void GetPlatform(string result)
    {
        if (string.IsNullOrEmpty(result))
        {
            zzLogger.LogError("GetPlatform result is null");

            return;
        }

        JsonData data = JsonMapper.ToObject(result);
        if (data == null)
        {
            zzLogger.LogError("GetPlatform JsonData is null");

            return;
        }

        GlobalData.mPlatformName = data["platform"].ToJson();
        GlobalData.mPackageName = data["package"].ToJson();

        // 2.读取资源配置和版本信息
        ConfigData.Instance.LoadConfigData(LoadConfigFinished);
    }

    void LoadConfigFinished()
    {
        // 3.播放开场视频
        Handheld.PlayFullScreenMovie("", Color.black, 
            FullScreenMovieControlMode.CancelOnInput | 
            FullScreenMovieControlMode.CancelOnInput);

        // 4.整理服务器列表
        List<ServerItemInfo> list = new List<ServerItemInfo>();
        for (int i = 0; i < 10; ++i)
        {
            ServerItemInfo info = new ServerItemInfo();
            info.mServerIp = "127.0.0.1";
            info.mServerPort = 20013;
            info.mServerName = "服务器-" + (i + 1);
            info.mServerStatus = 1;

            list.Add(info);
        }

        // 5.显示登陆界面
//         Transform panel = ViewManager.Instance.OpenUI("LoginUIPanel");
//         if (panel != null)
//         {
//             panel.GetComponent<LoginUIPanel>().ShowServerList(list);
//         }
    }

    void OnApplicationPause(bool isPause)
    {
        if (isPause)
        {
            zzLogger.Log("Game Pause");
        }
        else
        {
            zzLogger.Log("Game Resume");
        }
    }

    void OnApplicationQuit()
    {
        zzLogger.Log("Game Quit");
    }
}