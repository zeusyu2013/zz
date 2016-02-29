using UnityEngine;
using System.Collections;
using UnityEngine.Advertisements;
using System;

public class ADManager : ISingleton<ADManager>
{
    public void ShowAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }

    public void ShowRewardedAd()
    {
        if (Advertisement.IsReady("rewardedVideoZone"))
        {
            ShowOptions options = new ShowOptions { resultCallback = ShowResultCallback };
            Advertisement.Show("rewardedVideoZone", options);
        }
    }

    public void ShowResultCallback(ShowResult sr)
    {
        if (sr == ShowResult.Failed)
        {
            // 重试或者取消
        }
        else if (sr == ShowResult.Skipped)
        {
            // 跳过
        }
        else
        {
            // 成功播放完成，发放奖励
        }
    }
}