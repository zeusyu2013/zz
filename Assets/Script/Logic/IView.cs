using UnityEngine;
using System.Collections;

public class IView : MonoBehaviour
{
    public Transform mTrans;
    public bool mLoadFinished = false;

    /// <summary>
    /// 加载
    /// </summary>
    public virtual void OnLoad()
    {
        mTrans = transform;
    }

    /// <summary>
    /// 显示
    /// </summary>
    public virtual void OnShow()
    {
        mTrans.gameObject.SetActive(true);
    }

    /// <summary>
    /// 隐藏
    /// </summary>
    public virtual void OnHide() { }

    /// <summary>
    /// 销毁
    /// </summary>
    public virtual void OnViewDestroy()
    {
        if (mTrans == null)
        {
            return;
        }

        mTrans.gameObject.SetActive(false);

        GameObject.Destroy(mTrans.gameObject, 2.0f);
    }

    public T GetComponent<T>(string path)
    {
        return mTrans.FindChild(path).GetComponent<T>();
    }

    public void DestroyComponent(Component com)
    {
        if (com == null)
        {
            return;
        }
        
        com = null;
    }
}