using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownUI : BaseUIElement
{
    public override void OnAwake()
    {
        
    }

    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnLeftArrow;
    [SerializeField] private Button btnRightArrow;
    [SerializeField] private Transform townDetails;

    [SerializeField] private CanvasGroup adoptGroup;

    public void Init()
    {
        btnExit.onClick.AddListener(ModeTownManager.Instance.OutModeTown);
        btnLeftArrow.onClick.AddListener(ModeTownManager.Instance.BackToBeforeTown);
        btnRightArrow.onClick.AddListener(ModeTownManager.Instance.NextToAfterTown);
        
        ModeTownManager.Instance.OnBeforeDoMoveIsland += ModeTownManager_OnBeforeDoMoveIsland;
        ModeTownManager.Instance.OnAfterDoMoveIsland += ModeTownManager_OnAfterDoMoveIsland;
        ModeTownManager.Instance.OnInModeTown += ModeTownManager_OnInModeTown;
        ModeTownManager.Instance.OnOutModeTown += ModeTownManager_OnOutModeTown;

    }

    private void ModeTownManager_OnOutModeTown()
    {
        adoptGroup.blocksRaycasts = false;
    }

    private void ModeTownManager_OnInModeTown()
    {
        adoptGroup.blocksRaycasts = true;
    }

    private void ModeTownManager_OnAfterDoMoveIsland(int currentLevel, int maxLevel)
    {
        if (currentLevel > 0)
            btnLeftArrow.gameObject.SetActive(true);

        if (currentLevel < maxLevel)
            btnRightArrow.gameObject.SetActive(true);
        
        townDetails.gameObject.SetActive(true);
    }

    private void ModeTownManager_OnBeforeDoMoveIsland(int currentLevel, int maxLevel)
    {
        btnLeftArrow.gameObject.SetActive(false);
        btnRightArrow.gameObject.SetActive(false);
        townDetails.gameObject.SetActive(false);
    }
}
