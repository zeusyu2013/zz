using UnityEngine;
using System.Collections;

public class ItemDefine : MonoBehaviour 
{
    public enum ItemType
    {
        ItemType_Unknown    = 0,
        ItemType_Consume    = 1,
        ItemType_Equip      = 2,
        ItemType_Piece      = 3,
    }

    public enum ItemQuality
    {
        ItemQuality_Unknown = 0,
        ItemQuality_White   = 1,
        ItemQuality_Green   = 2,
        ItemQuality_Blue    = 3,
        ItemQuality_Purple  = 4,
        ItemQuality_Orange  = 5,
        ItemQuality_Red     = 6,
    }


}