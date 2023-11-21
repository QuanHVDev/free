using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListLevelUI : BaseUIElement
{
    [SerializeField] private BtnLevelUI btnLevelUI;
    [SerializeField] private Transform content;

    public void Init(DataLevelsSO data)
    {
        foreach (Transform trans in content)
        {
            if (btnLevelUI.transform != trans)
            {
                Destroy(trans.gameObject);
            }
        }

        btnLevelUI.gameObject.SetActive(true);
        for (int i = 0; i < data.MapManagers.Count; i++)
        {
            for (int j = 0; j < data.MapManagers[i].GetCountMaps(); j++)
            {
                var btn = Instantiate(btnLevelUI, content);
                btn.Init(i, j);
            }
        }
        
        btnLevelUI.gameObject.SetActive(false);
    }

    public override void OnAwake()
    {
        
    }
}
