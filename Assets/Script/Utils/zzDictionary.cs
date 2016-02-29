using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class zzDictionary<K, V> : Dictionary<K, V>
{
    public List<K> KeyList
    {
        get
        {
            return new List<K>(Keys);
        }
    }
}