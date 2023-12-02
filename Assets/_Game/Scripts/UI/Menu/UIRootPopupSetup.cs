using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIRootPopupSetup : MonoBehaviour
{
    [SerializeField] MainUIManager mainUIManager;
    [SerializeField] HomeUIManager homeUIManager;
    private void Start()
    {
        mainUIManager = MainUIManager.Instance;
        homeUIManager = HomeUIManager.Instance;
        
        mainUIManager.Init();
        homeUIManager.Init();
    }
}