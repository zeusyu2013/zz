using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class zzCommonUtils
{
    /// <summary>
    /// 获取当前时间（单位：秒）
    /// </summary>
    /// <returns></returns>
    public static double GetCurTimeSeconds()
    {
        TimeSpan span = DateTime.UtcNow - DateTime.Parse("1900-01-01");

        return span.TotalSeconds;
    }

    /// <summary>
    /// 获取当前时间（单位：毫秒）
    /// </summary>
    /// <returns></returns>
    public static double GetCurTimeMilliseconds()
    {
        TimeSpan span = DateTime.UtcNow - DateTime.Parse("1900-01-01");

        return span.TotalMilliseconds;
    }

    /// <summary>
    /// 保留num位小数
    /// </summary>
    /// <param name="f"></param>
    /// <param name="num"></param>
    /// <returns></returns>
    public static string DigitsFloat(float f, int num)
    {
        return Math.Round(f, num).ToString();
    }

    /// <summary>
    /// 添加组件
    /// </summary>
    /// <param name="type"></param>
    public static void AddComponent(string com, string parent = "")
    {
        if (string.IsNullOrEmpty(com))
        {
            zzLogger.LogError("AddComponent com name is null");

            return;
        }

        GameObject go = null;
        if (string.IsNullOrEmpty(parent))
        {
            go = GameObject.Find(com);
            if (null == go)
            {
                go = new GameObject(com);
            }
        }
        else
        {
            go = GameObject.Find(parent);
            if (go == null)
            {
                return;
            }
        }

        if (go.GetComponent(com) == null)
        {
            go.AddComponent(System.Type.GetType(com));
        }
    }

    /// <summary>
    /// 移除组件
    /// </summary>
    /// <param name="com"></param>
    public static void RemoveComponent(string com)
    {
        if (string.IsNullOrEmpty(com))
        {
            zzLogger.LogError("RemoveComponent com name is null");

            return;
        }

        GameObject go = GameObject.Find(com);
        if (go == null)
        {
            zzLogger.LogError("com " + com + "is not exists");

            return;
        }

        GameObject.Destroy(go);
    }

    /// <summary>
    /// 字符串高效拼接
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public static string StringBuilder(params object[] args)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < args.Length; ++i)
        {
            sb.Append(args[i]);
        }

        return sb.ToString();
    }

    /// <summary>
    /// 字符串分割
    /// </summary>
    /// <param name="str"></param>
    /// <param name="split"></param>
    /// <returns></returns>
    public List<object> StringSplit(string str, char split = ',')
    {
        if (string.IsNullOrEmpty(str))
        {
            return null;
        }

        List<object> objs = new List<object>();

        string[] strs = str.Split(split);
        for (int i = 0; i < strs.Length; ++i)
        {
            objs.Add(strs[i]);
        }

        return objs;
    }
}