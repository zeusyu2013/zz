using UnityEngine;
using System.Collections;

public class StoreManager : ISingleton<StoreManager>
{
    private string ACCOUNT = "account";
    private string PASSWORD = "password";
    private string MUSICSWITCH = "musicswitch";

    /// <summary>
    /// 玩家账号
    /// </summary>
    /// <param name="account"></param>
    public string Account
    {
        set
        {
            PlayerPrefs.SetString(ACCOUNT, value);
        }
        get
        {
            return PlayerPrefs.GetString(ACCOUNT, "");
        }
    }

    /// <summary>
    /// 玩家密码
    /// </summary>
    public string Password
    {
        set
        {
            PlayerPrefs.SetString(PASSWORD, value);
        }
        get
        {
            return PlayerPrefs.GetString(PASSWORD, "");
        }
    }

    /// <summary>
    /// 音效开关
    /// </summary>
    public bool MusicSwitch
    {
        set
        {
            PlayerPrefs.SetInt(MUSICSWITCH, value ? 1 : 0);
        }
        get
        {
            return PlayerPrefs.GetInt(MUSICSWITCH) > 0 ? true : false;
        }
    }
}