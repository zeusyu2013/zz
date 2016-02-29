using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class ConfigData : MonoBehaviour
{
    private static ConfigData _Instance = null;
    public static ConfigData Instance
    {
        private set { }
        get
        {
            return _Instance;
        }
    }

    public delegate void LoadConfigFinished();
    public LoadConfigFinished mLoadConfigFinished;

    private string mConfigIp = "";
    private string mConfigPath = "";
    private Dictionary<string, string> mConfigDataDic = null;

    void Start()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);

        mConfigDataDic = new Dictionary<string, string>();

        mConfigIp = "http://127.0.0.1/";
        mConfigPath = mConfigIp + "zz/Config.xml";
    }

    /// <summary>
    /// www读取服务器上配置
    /// </summary>
    public void LoadConfigData(LoadConfigFinished callback)
    {
        if (string.IsNullOrEmpty(mConfigPath))
        {
            zzLogger.LogError("LoadConfigData() args path is null");

            return;
        }

        if (callback != null)
        {
            mLoadConfigFinished = callback;
        }

        StartCoroutine(LoadConfigDataXml());
    }

    IEnumerator LoadConfigDataXml()
    {
        WWW www = new WWW(mConfigPath);

        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            zzLogger.LogError("LoadConfigDataXml() error:" + www.error);
        }
        else
        {
            string configText = www.text;
            if (string.IsNullOrEmpty(configText))
            {
                zzLogger.LogError("LoadConfigDataXml() www.text is null");
            }
            else
            {
                ParseConfigData(configText);
            }
        }

        if (mLoadConfigFinished != null)
        {
            mLoadConfigFinished();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configText"></param>
    private void ParseConfigData(string configText)
    {
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(configText);

        XmlNode node = doc.SelectSingleNode("Config");
        if (node == null)
        {
            return;
        }

        XmlNodeList list = node.ChildNodes;
        if (list == null)
        {
            return;
        }

        foreach (XmlNode no in list)
        {
            XmlElement element = no as XmlElement;
            if (element == null)
            {
                continue;
            }

            string name = element.Name;
            string value = element.GetAttribute("value");

            mConfigDataDic.Add(name, value);
        }
    }

    /// <summary>
    /// 获得配置值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public string GetConfigData(string key)
    {
        if (mConfigDataDic == null || mConfigDataDic.Count < 1)
        {
            zzLogger.LogError("mConfigDataDic is null or count < 1");

            return "";
        }

        if (string.IsNullOrEmpty(key))
        {
            zzLogger.LogError("GetConfigData() args key is null");

            return "";
        }

        if (!mConfigDataDic.ContainsKey(key))
        {
            zzLogger.LogError("GetConfigData() args " + key + " not found");

            return "";
        }

        return mConfigDataDic[key];
    }
}