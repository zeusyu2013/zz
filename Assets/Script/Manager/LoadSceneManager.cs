using UnityEngine;
using System.Collections;

public class LoadSceneManager : MonoBehaviour 
{
    private static LoadSceneManager _Instance = null;
    public static LoadSceneManager Instance
    {
        private set { }
        get
        {
            return _Instance;
        }
    }



    void Awake()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    public bool LoadSceneAsync(string sceneName)
    {
        if (string.IsNullOrEmpty(sceneName))
        {
            zzLogger.LogError("sceneName:" + sceneName + " is null");

            return false;
        }



        return true;
    }
}
