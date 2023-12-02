using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardUIManager : SingletonBehaviourDontDestroy<RewardUIManager>
{
    public int DiamondNeedShow { get; private set; } = 0;
    private RewardCanvas rewardCanvas;
    
    public void Init()
    {
        CallAnimReward(); 
    }

    public void CallAnimReward()
    {
        if (DiamondNeedShow == 0) return;
        StartCoroutine(RunAnimAsync());
    }

    private IEnumerator RunAnimAsync()
    {
        yield return new WaitUntil(() => UIRoot.Ins.Get<RewardCanvas>());
        rewardCanvas = UIRoot.Ins.Get<RewardCanvas>();
        rewardCanvas.RunAnim(DiamondNeedShow);
        DiamondNeedShow = 0;
    }

    public void AddDiamondNeedShow(int value)
    {
        DiamondNeedShow += value;
    }
}
