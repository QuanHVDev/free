using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

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
    private UserDataController udc;

    [Header("Config Data")] 
    [SerializeField] private MapManager map;

    public CameraManager Camera => camera;

    private void Start()
    {
        udc = UserDataController.Instance;
        
        currentLevel = 0;
        if (map) {
            currentMapManager = map;
            currentMapManager.InitData();
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
        
        SetUpTutorial();
    }

    private void SetUpTutorial()
    {
        var process = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        if (!process.IsShowSwipe && !process.IsShowHint)
        {
            gamePlayUI.TurnOffTutorialUI();
        }
        else
        {
            if (process.IsShowHint)
            {
                Transform s = gamePlayUI.GetIconPeopleManagerUI().Icons[1].transform;
                var list = gamePlayUI.GetIconPeopleManagerUI().Icons[1].Targets;
                Transform e = list[Random.Range(0, list.Count)].transform;
                gamePlayUI.SetHint(s, e);
            }
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
            currentMapManager.InitData();
            SetUpActionCurrentMapManager();
        }
        else
        {
            currentMapManager.InitData();
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
                Debug.Log($"{currentLevel} - {dataLevelsSO.CountLevel()} - {indexMapManager}");
                if (currentLevel >= dataLevelsSO.CountLevel()) indexMapManager = 0;
            }
        };

        currentMapManager.OnSetCatTarget += camera.GetLookTargetCamera;
        currentMapManager.OnCameraLookTarget += (x, y)=> {
            camera.GetElementCameraPrev().VirtualCamera.gameObject.SetActive(false);
            camera.ChangeState(x, y);
        };
        currentMapManager.OnCompletePath += () => {
            var element = camera.GetElementCameraPrev();
            camera.ChangeState(element.triggerNameAnimationState, CameraManager.StateVirtualCamera.Wait);
            element.VirtualCamera.gameObject.SetActive(true);
            if (isCheckShowSwipe)
            {
                var process = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
                if (process.IsShowSwipe)
                {
                    gamePlayUI.ShowUISwipe(true);
                }
            }
        };

        currentMapManager.OnMapBusy += gamePlayUI.EnableRaycastTargetIconPeople;
        currentMapManager.OnCorrect += gamePlayUI.SetSmoothBar;
    }

    private bool isCheckShowSwipe = false;

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
