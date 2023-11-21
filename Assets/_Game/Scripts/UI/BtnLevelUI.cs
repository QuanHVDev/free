using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BtnLevelUI : MonoBehaviour
{
    private int level, step;
    [SerializeField] private TMP_Text txt;
    [SerializeField] private Button btn;
    public void Init(int level, int step)
    {
        this.level = level;
        this.step = step;
        txt.text = $"Level {level + 1}-{step + 1}";
        this.name = txt.text;
    }

    private void Start()
    {
        btn.onClick.AddListener(() =>
        {
            GameManager.Instance.SpawnLevel(level, step);
        });
    }
}