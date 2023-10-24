using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class GameManager : SingletonBehaviour<GameManager> {
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private Transform spawnMap;
    
    [SerializeField] private DataLevelsSO dataLevelsSO;
    [SerializeField] private int indexMapManager;
    [SerializeField] private CameraManager camera;
    
    private MapManager currentMapManager;
    
    // health
    private int maxHealth = 3;
    private int currentHealth;

    [Header("Config Data")] 
    [SerializeField] private MapManager map;
    
    
    private void Start() {
        currentLevel = 0;
        if (map) {
            currentMapManager = map;
            SetUpActionCurrentMapManager();
            var element = camera.GetVirtualCameraFree(currentMapManager.GetCurrentCameraPosition());
            camera.MoveCameraToVirtualCamera(element);
            gamePlayUI.GetIconHomeManagerUI().Add(currentMapManager, gamePlayUI);
            gamePlayUI.GetIconPeopleManagerUI()
                .SetAllHomesForIcon(gamePlayUI.GetIconHomeManagerUI().GetCurrentIconForMap());
            gamePlayUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());

            LoadHealth();
        }
        else {
            SpawnLevel();
        }
    }

    public void SpawnLevel() {
        StartCoroutine(SpawnLevelAsync());
    }

    private IEnumerator SpawnLevelAsync() {
        if (currentMapManager) {
            gamePlayUI.GetIconHomeManagerUI().FinishMap();
            gamePlayUI.GetIconPeopleManagerUI().FinishMap();
            if (currentMapManager.currentIndexMap >= currentMapManager.GetCountMaps()) {
                camera.ReturnVirtualCameraToOrigin();
                yield return new WaitUntil(() => {
                    return camera.state == CameraManager.StateVirtualCamera.Finish;
                });
                Destroy(currentMapManager.gameObject);
            }
        }

        LoadLevel(indexMapManager);
        gamePlayUI.GetIconHomeManagerUI().Add(currentMapManager, gamePlayUI);
        gamePlayUI.GetIconPeopleManagerUI().SetAllHomesForIcon(gamePlayUI.GetIconHomeManagerUI().GetCurrentIconForMap());
        gamePlayUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());
        
        LoadHealth();
    }

    //public void SpawnLevel() {
        
        //camera.ResetToOriginPosition();
        // var inv = UserDataController.Instance.GetData<Inventory>(UserDataKeys.USER_INVENTORY, out _);
        // if (inv.currentLevel >= data.lvls.Count) {
        //     gamePlayUI.LoadContent(inv.currentLevel);
        // }
        // else {
        //     gamePlayUI.LoadContent(currentLevel);
        // }
    //}
    
    private void LoadLevel(int index) {
        if (!currentMapManager || currentMapManager.currentIndexMap >= currentMapManager.GetCountMaps()) {
            currentMapManager = Instantiate(dataLevelsSO.MapManagers[index], spawnMap);
            SetUpActionCurrentMapManager();
        }
        
        var element = camera.GetVirtualCameraFree(currentMapManager.GetCurrentCameraPosition());
        camera.MoveCameraToVirtualCamera(element);
    }

    private void SetUpActionCurrentMapManager() {
        currentMapManager.OnFinishLevel += gamePlayUI.ShowUIWin;
        currentMapManager.OnFinishLevel += () => {
            currentMapManager.currentIndexMap++;
            currentLevel++;
            if (currentMapManager.currentIndexMap >= currentMapManager.GetCountMaps()) {
                currentMapManager.RemoveCurrentNavmeshData();
                indexMapManager++;
                if (currentLevel >= dataLevelsSO.CountLevel()) indexMapManager = 0;
            }
        };

        currentMapManager.OnSetCatTarget += camera.GetLookTargetCamera;
        currentMapManager.OnCameraLookTarget += (x, y)=> {
            camera.GetElementCameraPrev().VirtualCamera.gameObject.SetActive(false);
            camera.ChangeState(x, y);
        };
        currentMapManager.OnCompletePath += () => {
            camera.GetElementCameraPrev().VirtualCamera.gameObject.SetActive(true);
        };

        currentMapManager.OnMapBusy += gamePlayUI.EnableRaycastTargetIconPeople;
    }

    private int currentLevel;

    private void LoadHealth() {
        currentHealth = maxHealth;
        gamePlayUI.GetIconHealthManagerUI().Init(currentHealth);
    }

    public void Incorrent() {
        --currentHealth;
        gamePlayUI.GetIconHealthManagerUI().LoseHealth();
    }

    public MapManager GetCurrentMapManager() {
        return currentMapManager;
    }
}