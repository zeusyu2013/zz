using UnityEngine;
using System.Collections;

public class InitManager : MonoBehaviour
{
    private static InitManager _Instance = null;
    public static InitManager Instance
    {
        private set { }
        get
        {
            return _Instance;
        }
    }

    public string mConfigPath = "http://localhost/config.xml";

    void Awake()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 初始化系统配置
    /// </summary>
    /// <returns></returns>
    public bool Init()
    {
        double curMilliseconds = zzCommonUtils.GetCurTimeSeconds();
        mConfigPath += "/?p=" + curMilliseconds;

        WWW www = new WWW(mConfigPath);

        StartCoroutine(WaitForConfig(www));

        return true;
    }

    IEnumerator WaitForConfig(WWW www)
    {
        yield return www;

        if (www.error != null)
        {
            zzLogger.LogError("www config is error, error = " + www.error);
        }
        else
        {
            if (www.isDone)
            {
                string configText = www.text;
                if (!string.IsNullOrEmpty(configText))
                {
                    // 解析config.xml

                }
            }
        }
    }
}