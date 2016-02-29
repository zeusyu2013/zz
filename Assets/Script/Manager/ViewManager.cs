using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewManager : ISingleton<ViewManager>
{
    private GameObject mViewParent;
    private Dictionary<string, IView> mViewsDic = new Dictionary<string, IView>();
    
    void Awake()
    {
        mViewParent = GameObject.Find("UI Root/Camera");
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    /// <param name="uiName"></param>
    public IView OpenUI<T>() where T : Component
    {
        string name = typeof(T).ToString();

        if (string.IsNullOrEmpty(name))
        {
            zzLogger.LogError("uiName is null");

            return null;
        }

        if (mViewsDic.ContainsKey(name))
        {
            return mViewsDic[name];
        }

        GameObject prefab = Resources.Load("UI/UIPrefab/" + name) as GameObject;
        if (prefab == null)
        {
            zzLogger.LogError(string.Format("uiprefab {0} is null", name));

            return null;
        }

        GameObject ui = NGUITools.AddChild(mViewParent, prefab);
        if (ui == null)
        {
            zzLogger.LogError("ui is null");

            return null;
        }

        ui.name = name;
        IView view = ui.AddComponent<T>() as IView;
        view.OnLoad();

        mViewsDic.Add(name, view);

        return view;
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    /// <param name="uiName"></param>
    public void CloseUI(string uiName)
    {
        if (string.IsNullOrEmpty(uiName))
        {
            return;
        }
        
        if (!mViewsDic.ContainsKey(uiName))
        {
            return;
        }

        mViewsDic[uiName].OnViewDestroy();
    }

    /// <summary>
    /// 关闭所有界面
    /// </summary>
    public void CloseAllUI()
    {

    }
}