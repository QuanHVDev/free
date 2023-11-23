using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneUI : BaseUIElement
{
    [SerializeField] private Image imgFill;

    public void SetFill(float value)
    {
        imgFill.fillAmount = value;
    }
    
    public override void OnAwake()
    {
    }
}
