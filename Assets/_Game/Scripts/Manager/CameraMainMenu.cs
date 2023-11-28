using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMainMenu : SingletonBehaviour<CameraMainMenu>
{
    private const string NAMEANIM_SHOW_ALL = "vCamera_ShowAllMap";
    private const string NAMEANIM_LOOK_TARGET = "vCamera_LTarget";
    
    [SerializeField] private Camera camMain;
    [SerializeField] private Camera camMainUI;

    private Animator animatorCameraMain;

    private void Start()
    {
        animatorCameraMain = camMain.GetComponent<Animator>();
        camMainUI.gameObject.SetActive(true);
    }

    public void InModeTown()
    {
        animatorCameraMain.Play(NAMEANIM_LOOK_TARGET);
    }

    public void OutModeTown()
    {
        animatorCameraMain.Play(NAMEANIM_SHOW_ALL);
    }
}
