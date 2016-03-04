using UnityEngine;
using System.Collections;

public class ObjectPoolManager : ISingleton<ObjectPoolManager>
{
    private zzDictionary<int, GameObject> mObjects = new zzDictionary<int, GameObject>();

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public GameObject CreateObject(int id)
    {
        if (id < 1)
        {
            return null;
        }

        return null;
    }

    public void RemoveObject(int id)
    {
        if (!mObjects.ContainsKey(id))
        {
            return;
        }

        Resources.UnloadAsset(mObjects[id]);
        mObjects[id] = null;

        mObjects.Remove(id);
    }
}