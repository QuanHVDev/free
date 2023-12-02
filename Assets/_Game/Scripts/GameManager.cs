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
        currentMode.Init();
        LoadSceneUIManager.Instance.SetOffLoading();
        currentMode = null;
    }

    private ModeManager currentMode;

    public void SetModeManager(ModeManager currentMode)
    {
        this.currentMode = currentMode;
    }
}

public enum GameState
{
    MainMenu,
    Loading,
    InMode
}