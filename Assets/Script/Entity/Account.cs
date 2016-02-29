using UnityEngine;
using KBEngine;
using System;
using System.Collections;

public class Account : Entity 
{
    public Account()
    {

    }

    public override void __init__()
    {
        base.__init__();
        baseCall("reqPlayerInfo");
    }

    public void onCreatePlayerResult(Byte retCode, object info)
    {

    }

    public void reqCreatePlayer(Byte playerType, string playerName)
    {
        baseCall("reqCreatePlayer", playerType, playerName);
    }

    public void enterGame(UInt64 dbid)
    {
        baseCall("enterGame", dbid);
    }
}