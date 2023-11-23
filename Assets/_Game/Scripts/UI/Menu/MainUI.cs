using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainUI : BaseUIElement
{
    [SerializeField] private TMP_Text txtDiamond;
    public override void OnAwake()
    {
        
    }

    public void UpdateTextDiamond(int diamond)
    {
        txtDiamond.text = diamond.ToString();
    }
}
