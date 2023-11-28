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
        ModeTownManager.Instance.OnInModeTown += Hide;
        ModeTownManager.Instance.OnOutModeTown += ()=>
        {
            Show();
        };
    }

    public override void OnAwake()
    {
        
    }

    public void Init()
    {
        Show();
    }

    public void SetTextLevel(string txt)
    {
        txtPlayLevel.text = "Lvl. " + txt;
    }
}
