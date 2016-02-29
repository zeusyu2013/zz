using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum TimerType
{
    TimerType_Once = 1,
    TimerType_Repeat = 2,
}

public delegate void TimeCallback();

public class Timer
{
    public string mTimerName;
    public double mTimerSpace;
    public double mTimerNextCallback;
    public TimerType mTimerType;
    public TimeCallback mTimeCallback;
}

public class TimerManager : ISingleton<TimerManager>
{
    /// <summary>
    /// Timer表
    /// </summary>
    private Dictionary<string, Timer> mTimerDic;

    /// <summary>
    /// 移除Timer缓存
    /// </summary>
    private List<string> mRemoveTimerNames;

    void Awake()
    {
        mTimerDic = new Dictionary<string, Timer>();
        mRemoveTimerNames = new List<string>();
    }

    /// <summary>
    /// 增加一个一次性定时器
    /// </summary>
    /// <param name="timerName"></param>
    /// <param name="timerSpace"></param>
    /// <param name="callback"></param>
    public void AddOnceTimer(string timerName, float timerSpace, TimeCallback callback = null)
    {
        if (mTimerDic.ContainsKey(timerName))
        {
            zzLogger.Log("timer " + timerName + " repeat");

            return;
        }

        Timer timer = new Timer();
        timer.mTimerName = timerName;
        timer.mTimerSpace = timerSpace;
        timer.mTimerNextCallback = zzCommonUtils.GetCurTimeMilliseconds() + (double)(timerSpace * 1000.0f);
        timer.mTimerType = TimerType.TimerType_Once;
        timer.mTimeCallback = callback;

        mTimerDic.Add(timerName, timer);
    }

    /// <summary>
    /// 增加一个重复定时器
    /// </summary>
    /// <param name="timerName"></param>
    public void AddRepeatTimer(string timerName, float timerSpace, TimeCallback callback = null)
    {
        if (mTimerDic.ContainsKey(timerName))
        {
            zzLogger.Log("timer " + timerName + " repeat");

            return;
        }

        Timer timer = new Timer();
        timer.mTimerName = timerName;
        timer.mTimerSpace = timerSpace;
        timer.mTimerNextCallback = zzCommonUtils.GetCurTimeMilliseconds() + (double)(timerSpace * 1000.0f);
        timer.mTimerType = TimerType.TimerType_Repeat;
        timer.mTimeCallback = callback;

        mTimerDic.Add(timerName, timer);
    }

    /// <summary>
    /// 删除一个定时器
    /// </summary>
    /// <param name="timerName"></param>
    public void RemoveTimer(string timerName)
    {
        if (!mTimerDic.ContainsKey(timerName))
        {
            zzLogger.Log(timerName + " is not exist");

            return;
        }

        // 移除Timer时放入缓存中，等待一个callback后删除
        mRemoveTimerNames.Add(timerName);
    }

    void FixedUpdate()
    {
        if (mTimerDic == null || mTimerDic.Count < 1)
        {
            return;
        }

        foreach(Timer timer in mTimerDic.Values)
        {
            if (timer == null)
            {
                continue;
            }

            double curMilliseconds = zzCommonUtils.GetCurTimeMilliseconds();
            if (curMilliseconds < timer.mTimerNextCallback)
            {
                continue;
            }

            if (timer.mTimeCallback == null)
            {
                continue;
            }

            timer.mTimeCallback();

            // 不能在遍历中直接删除，使用缓存处理
            if (timer.mTimerType == TimerType.TimerType_Once)
            {
                mRemoveTimerNames.Add(timer.mTimerName);
            }
            else if (timer.mTimerType == TimerType.TimerType_Repeat)
            {
                timer.mTimerNextCallback += timer.mTimerSpace * 1000.0f;
            }
        }

        for (int i = 0; i < mRemoveTimerNames.Count; ++i)
        {
            string timerName = mRemoveTimerNames[i];
            if (string.IsNullOrEmpty(timerName))
            {
                continue;
            }

            mTimerDic.Remove(timerName);
        }

        mRemoveTimerNames.Clear();
    }
}