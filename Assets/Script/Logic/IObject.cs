using UnityEngine;
using System.Collections;

public class IObject
{
    public GameObject mObjGo;
    public Transform mObjTrans;

    public int mObjId;

    public void DestroySelf()
    {
        mObjGo = null;
        mObjTrans = null;

        mObjId = int.MinValue;
    }
}