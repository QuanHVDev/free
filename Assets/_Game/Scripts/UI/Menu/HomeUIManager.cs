using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeUIManager : SingletonBehaviour<HomeUIManager>
{
    private HomeUI homeUI;

    public void Init()
    {
        var pro = UserDataController.Instance.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        homeUI = UIRoot.Ins.Get<HomeUI>();
        homeUI.Init();
        homeUI.SetTextLevel($"{pro.currentLevel+1}-{pro.currentStep+1}");
    }

    public void PlayLevel()
    {
        
    }
}
