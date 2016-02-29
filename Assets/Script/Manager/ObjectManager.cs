using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectManager : ISingleton<ObjectManager>
{
    private zzDictionary<string, IObject> mObjectsDic = new zzDictionary<string, IObject>();

    public void Clear()
    {
        if (mObjectsDic == null)
        {
            return;
        }

        List<string> keys = mObjectsDic.KeyList;
        for (int i = 0; i < keys.Count; ++i)
        {
            IObject obj = mObjectsDic[keys[i]];
            if (obj == null)
            {
                continue;
            }

            obj.DestroySelf();
            obj = null;
        }

        mObjectsDic.Clear();
    }
}