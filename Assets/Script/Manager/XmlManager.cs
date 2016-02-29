using UnityEngine;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

public class XmlManager
{
    private XmlManager() { }
    private static XmlManager _Instance = null;
    public static XmlManager Instance
    {
        private set { }
        get
        {
            if (_Instance == null)
            {
                _Instance = new XmlManager();
            }
            return _Instance;
        }
    }

    private string mXmlPath = "Configure/";

    private Dictionary<string, string> mCN;
    public Dictionary<string, string> CN
    {
        get { return mCN; }
    }

    private Dictionary<string, string> mPlayerBase;
    public Dictionary<string, string> PlayerBase
    {
        get { return PlayerBase; }
    }

    /// <summary>
    /// 获取Xml文本
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    public string GetXmlText(string xml)
    {
        if (string.IsNullOrEmpty(xml))
        {
            zzLogger.Log(xml + ".xml is null");

            return "";
        }

        TextAsset textAsset = Resources.Load(mXmlPath + xml) as TextAsset;
        if (textAsset == null)
        {
            zzLogger.Log("xmlText is null");

            return "";
        }

        string text = textAsset.text;
        if (string.IsNullOrEmpty(text))
        {
            zzLogger.Log("text is null");

            return "";
        }

        return text;
    }

    /// <summary>
    /// 加载CN
    /// </summary>
    /// <param name="xml"></param>
    /// <returns></returns>
    private bool LoadCN(string xml)
    {
        string text = GetXmlText(xml);
        if (string.IsNullOrEmpty(text))
        {
            return false;
        }

        mCN = new Dictionary<string, string>();

        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);

        XmlNode node = doc.SelectSingleNode("CN");
        if (node == null)
        {
            return false;
        }

        XmlNodeList nodeList = node.ChildNodes;

        foreach (XmlNode no in nodeList)
        {
            XmlElement element = no as XmlElement;
            if (element == null)
            {
                continue;
            }

            string id = element.GetAttribute("ID");
            string stringText = element.GetAttribute("Text");

            mCN.Add(id, stringText);
        }

        return true;
    }

    /// <summary>
    /// 加载全部xml文件
    /// </summary>
    /// <returns></returns>
    public bool LoadAll()
    {
        if (!LoadCN("CN"))
        {
            return false;
        }

        return true;
    }
}