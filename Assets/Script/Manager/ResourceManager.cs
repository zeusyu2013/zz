using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    #region <>单例<>
    private static ResourceManager _Instance = null;
    public static ResourceManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    #endregion

    #region <>启动<>
    void Awake()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // 已解压的Asset列表 [prefabPath, asset]
    private Dictionary<string, Object> dicAsset = new Dictionary<string, Object>();

    // "正在"加载的资源列表 [prefabPath, www]
    private Dictionary<string, WWW> dicLoadingReq = new Dictionary<string, WWW>();

    public delegate void LoadObjectCallback(string name, Object o);

    public void GetUIObject(string name, LoadObjectCallback callback)
    {
        LoadAsync("UIPrefab/" + name, callback);
    }

    #region <>私有方法<>
    // name表示prefabPath，eg:Prefab/Pet/ABC
    public void LoadAsync(string name, LoadObjectCallback callback)
    {
        LoadAsync(name, typeof(Object), callback);
    }

    // name表示prefabPath，eg:Prefab/Pet/ABC
    public void LoadAsync(string name, System.Type type, LoadObjectCallback callback)
    {
        // 如果已经下载，则返回
        if (dicAsset.ContainsKey(name))
            return;

        // 如果正在下载，则返回
        if (dicLoadingReq.ContainsKey(name))
            return;

        // 添加引用
        RefAsset(name);

        // 如果没下载，则开始下载
        StartCoroutine(AsyncLoadCoroutine(name, type, callback));
    }

    private IEnumerator AsyncLoadCoroutine(string name, System.Type type, LoadObjectCallback callback)
    {
        string assetBundleName = "";// GlobalSetting.ConvertToAssetBundleName(name);
        string url = "";// GlobalSetting.ConverToFtpPath(assetBundleName);
        int verNum = 0;// GameApp.GetVersionManager().GetVersionNum(assetBundleName);

        Debug.Log("WWW AsyncLoad name =" + assetBundleName + " versionNum = " + verNum);
        if (!Caching.IsVersionCached(url, verNum))
        {
            Debug.Log("Version Is not Cached, which will download from net!");
        }

        WWW www = WWW.LoadFromCacheOrDownload(url, verNum);
        while (www.isDone == false)
        {
            yield return null;
        }

        AssetBundleRequest req = www.assetBundle.LoadAssetAsync(GetAssetName(name), type);
        while (req.isDone == false)
        {
            yield return null;
        }

        if (callback != null)
        {
            callback(GetAssetName(name), req.asset);
        }

        www.assetBundle.Unload(false);
        www.Dispose();
        www = null;
    }

    public bool IsResLoading(string name)
    {
        return dicLoadingReq.ContainsKey(name);
    }

    public bool IsResLoaded(string name)
    {
        return dicAsset.ContainsKey(name);
    }

    public WWW GetLoadingWWW(string name)
    {
        WWW www = null;
        dicLoadingReq.TryGetValue(name, out www);

        return www;
    }

    // 移除Asset资源的引用，name表示prefabPath
    public void UnrefAsset(string name)
    {
        dicAsset.Remove(name);
    }

    private string GetAssetName(string ResName)
    {
        int index = ResName.LastIndexOf('/');
        return ResName.Substring(index + 1, ResName.Length - index - 1);
    }

    public void UnloadUnusedAsset()
    {
//         bool effectNeedUnload = GameApp.GetEffectManager().UnloadAsset();
//         bool worldNeedUnload = GameApp.GetWorldManager().UnloadAsset();
//         bool sceneNeedUnload = GameApp.GetSceneManager().UnloadAsset();
//         if (effectNeedUnload || worldNeedUnload || sceneNeedUnload)
//         {
//             Resources.UnloadUnusedAssets();
//         }
    }

    // 根据资源路径添加资源引用，每个管理器管理自己的引用
    private void RefAsset(string name)
    {
//         // 模型之类的
//         if (name.Contains(GlobalSetting.CharacterPath))
//             GameApp.GetWorldManager().RefAsset(name);
//         // 图片之类的
//         else if (name.Contains(GlobalSetting.TexturePath))
//             GameApp.GetUIManager().RefPTexture(name);// 特效之类的
//         else if (name.Contains(GlobalSetting.EffectPath))
//             GameApp.GetEffectManager().RefAsset(name);
// 　　　　 else
//             Debug.LogWarning("<Res not ref> name = " + name);
    }
    #endregion
}