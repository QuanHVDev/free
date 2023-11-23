using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeUI : BaseUIElement
{
    [SerializeField] private TMP_Text txtPlayLevel;
    [SerializeField] private Button btnPlay;

    private void Start()
    {
        btnPlay.onClick.AddListener(LoadSceneUIManager.Instance.LoadPlayGame);
    }

    public override void OnAwake()
    {
        
    }

    public void Init()
    {
        btnPlay.onClick.AddListener(HomeUIManager.Instance.PlayLevel);
    }

    public void SetTextLevel(string txt)
    {
        txtPlayLevel.text = "Lvl. " + txt;
    }
}
