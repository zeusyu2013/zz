using KBEngine;
using UnityEngine;
using System.Collections;

public class zzNetUtil : MonoBehaviour 
{
    /// <summary>
    /// 发送消息到服务端
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public static void SendServerMsg(string eventName, params object[] args)
    {
        KBEngine.Event.fireIn(eventName, args);
    }

    /// <summary>
    /// 注册客户端消息
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="obj"></param>
    /// <param name="funcName"></param>
    public static void RegisterCustomMsg(string eventName, object obj, string funcName)
    {
        KBEngine.Event.registerIn(eventName, obj, funcName);
    }

    /// <summary>
    /// 注销客户端消息
    /// </summary>
    /// <param name="obj"></param>
    public static void UnregisterCustomMsg(object obj)
    {
        KBEngine.Event.deregisterIn(obj);
    }

    /// <summary>
    /// 注册服务端消息
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="obj"></param>
    /// <param name="funcName"></param>
    public static void RegisterServerMsg(string eventName, object obj, string funcName)
    {
        KBEngine.Event.registerOut(eventName, obj, funcName);
    }

    /// <summary>
    /// 注销服务端消息
    /// </summary>
    /// <param name="obj"></param>
    public static void UnregisterServerMsg(object obj)
    {
        KBEngine.Event.deregisterOut(obj);
    }
}
