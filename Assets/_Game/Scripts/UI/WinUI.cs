using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : BaseUIElement
{
    public Action OnHideLine;
    [SerializeField] private Button btnNextLevel;
    [SerializeField] private TMP_Text txtCoin;

    public override void OnAwake()
    {
    }

    private void Start()
    {
        btnNextLevel.onClick.AddListener(() =>
        {
            OnHideLine?.Invoke();
            ModeFindCatManager.Instance.NextLevel();
            Hide();
        });
    }

    public void SetValueDiamond(int value)
    {
        txtCoin.text = $"+{value}";
    }
}