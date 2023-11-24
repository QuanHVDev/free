using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIRootPopupSetup : MonoBehaviour
{
    [SerializeField] MainUIManager mainUIManager;
    [SerializeField] HomeUIManager homeUIManager;
    [SerializeField] ModeTownManager modeTownManager;
    private void Start()
    {
        mainUIManager = MainUIManager.Instance;
        homeUIManager = HomeUIManager.Instance;
        modeTownManager = ModeTownManager.Instance;
        
        mainUIManager.Init();
        homeUIManager.Init();
        modeTownManager.Init();
    }
}