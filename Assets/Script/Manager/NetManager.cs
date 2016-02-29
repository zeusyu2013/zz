using UnityEngine;

using System;
using KBEngine;
using System.Collections;

public class NetManager : MonoBehaviour 
{
    private static NetManager _Instance = null;
    public static NetManager Instance
    {
        private set { }
        get
        {
            return _Instance;
        }
    }

    private KBEngineApp mKBEngineApp = null;

    public bool mIsMultiThreads = true;
    public KBEngineApp.CLIENT_TYPE mClientType = KBEngineApp.CLIENT_TYPE.CLIENT_TYPE_MINI;
    public string mPersistentDataPath = "Application.persistentDataPath";
    public bool mIsSyncPlayer = true;
    public int mThreadUpdateHZ = 10;
    public int SEND_BUFFER_MAX = (int)KBEngine.NetworkInterface.TCP_PACKET_MAX;
    public int RECV_BUFFER_MAX = (int)KBEngine.NetworkInterface.TCP_PACKET_MAX;

    void Awake()
    {
        _Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        InitEvents();
    }

    public bool InitNet(string ip, int port)
    {
        KBEngineArgs args = new KBEngineArgs();
        args.ip = ip;
        args.port = port;
        args.clientType = mClientType;

        if (mPersistentDataPath == "Application.persistentDataPath")
        {
            args.persistentDataPath = Application.persistentDataPath;
        }
        else
        {
            args.persistentDataPath = mPersistentDataPath;
        }

        args.syncPlayer = mIsSyncPlayer;
        args.threadUpdateHZ = mThreadUpdateHZ;

        args.SEND_BUFFER_MAX = (UInt32)SEND_BUFFER_MAX;
        args.RECV_BUFFER_MAX = (UInt32)RECV_BUFFER_MAX;

        args.isMultiThreads = mIsMultiThreads;

        if (mIsMultiThreads)
        {
            mKBEngineApp = new KBEngineAppThread(args);
        }
        else
        {
            mKBEngineApp = new KBEngineApp(args);
        }

        // 初始化网络完成后，登陆到loingapp
        LoginToLoginApp();

        return true;
    }

    void InitEvents()
    {

    }

    void LoginToLoginApp()
    {
        
    }

    void OnDestroy()
    {
        mKBEngineApp.destroy();
    }

    void FixedUpdate()
    {
        // 单线程模式必须自己调用
        if (!mIsMultiThreads)
        {
            mKBEngineApp.process();
        }

        KBEngine.Event.processOutEvents();
    }
}
