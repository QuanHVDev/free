using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : SingletonBehaviour<MainUIManager>
{
    private MainUI mainUI;
    private int diamond;
    public void Init()
    {
        mainUI = UIRoot.Ins.Get<MainUI>();
    }

    public void ChangeValueDiamond(int value)
    {
        diamond = value;
        mainUI.UpdateTextDiamond(diamond);
    }

    public void AddValueDiamond(int value)
    {
        diamond += value;
        mainUI.UpdateTextDiamond(diamond);
    }
    
    
}