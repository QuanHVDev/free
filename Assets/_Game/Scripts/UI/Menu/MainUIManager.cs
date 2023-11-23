using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIManager : SingletonBehaviour<MainUIManager>
{
    private int diamond = 0;
    private MainUI mainUI;
    public void Init()
    {
        mainUI = UIRoot.Ins.Get<MainUI>();
        ChangeValueDiamond(0);
    }

    public bool ChangeValueDiamond(int value)
    {
        if (diamond + value < 0) return false;

        diamond += value;
        var inv = UserDataController.Instance.GetData<InventoryData>(UserDataKeys.USER_INVENTORY, out _);
        inv.diamond = diamond;
        UserDataController.Instance.SetData(UserDataKeys.USER_INVENTORY, inv);
        mainUI.UpdateTextDiamond(diamond);
        
        return true;
    }
}