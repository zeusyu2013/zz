using UnityEngine;
using System.Collections;

public class IObject
{
    /// <summary>
    /// 对象属性
    /// </summary>
    public GameObject mObjGo;
    public Transform mObjTrans;

    /// <summary>
    /// 对象唯一标示
    /// </summary>
    public int mObjId;

    /// <summary>
    /// 销毁
    /// </summary>
    public void DestroySelf()
    {
        mObjGo = null;
        mObjTrans = null;

        mObjId = int.MinValue;
    }
}