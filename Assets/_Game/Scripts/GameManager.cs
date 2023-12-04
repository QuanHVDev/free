using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : IronPirate.SingletonBehaviourDontDestroy<GameManager>
{
    private GameState state;

    private void Start()
    {
        state = GameState.MainMenu;
        ModeTownManager.Instance.Init();
        RewardUIManager.Instance.Init();
        MainUIManager.Instance.Init();
        UpdateDiamond();
    }

    private Coroutine Coroutine;

    public void ChangeState(GameState state)
    {
        this.state = state;
        if (Coroutine != null)
        {
            currentMode = null;
            StopCoroutine(Coroutine);
        }
        Coroutine = StartCoroutine(WaitSetMode());
    }

    private IEnumerator WaitSetMode()
    {
        yield return new WaitUntil(() => currentMode && LoadSceneUIManager.Instance.State == LoadSceneUIManager.LoadingState.Done);
        if (currentMode)
        {
            currentMode.Init();
            currentMode = null;
        }
        
        LoadSceneUIManager.Instance.SetOffLoading();
        var pro = UserDataController.Instance.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        
        yield return new WaitUntil(() => UIRoot.Ins.Get<MainUI>());
        MainUIManager.Instance.ChangeValueDiamond(pro.diamond - RewardUIManager.Instance.DiamondNeedShow);
        
        yield return new WaitUntil(() => UIRoot.Ins.Get<RewardCanvas>());
        RewardUIManager.Instance.Init();
    }

    private ModeManager currentMode;

    public void SetModeManager(ModeManager currentMode)
    {
        this.currentMode = currentMode;
    }

    public void UpdateDiamond()
    {
        var pro = UserDataController.Instance.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        MainUIManager.Instance.ChangeValueDiamond(pro.diamond);
    }
}

public enum GameState
{
    MainMenu,
    Loading,
    InMode
}