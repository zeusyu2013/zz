using UnityEngine;
using System.Collections;

public class ISingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _Instance = null;
    private static bool mApplicationQuitting = false;

    public static T Instance
    {
        private set { }
        get
        {
            if (mApplicationQuitting)
            {
                return null;
            }

            if (_Instance == null)
            {
                GameObject parent = GameObject.Find("GameMain");
                if (parent == null)
                {
                    return null;
                }

                GameObject go = new GameObject(typeof(T).ToString());
                go.transform.parent = parent.transform;

                _Instance = go.AddComponent<T>();
            }

            return _Instance;
        }
    }

    void OnDestroy()
    {
        mApplicationQuitting = true;
    }
}