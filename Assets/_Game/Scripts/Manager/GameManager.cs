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
        var pro = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        indexMapManager = pro.currentLevel;
        
        if (map) {
            currentMapManager = map;
            currentMapManager.InitData();
            SetUpActionCurrentMapManager();
            var element = camera.GetVirtualCameraFree(currentMapManager.GetCurrentCameraPosition());
            camera.MoveCameraToVirtualCamera(element, SetUpTutorial);
            gamePlayUI.GetIconHomeManagerUI().Add(currentMapManager, gamePlayUI);
            gamePlayUI.GetIconPeopleManagerUI()
                .SetAllHomesForIcon(gamePlayUI.GetIconHomeManagerUI().GetCurrentIconForMap());
            gamePlayUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());

            LoadHealth();
        }
        else {
            SpawnLevel();
        }
        
        gamePlayUI.UpdateTitle(indexMapManager, currentMapManager.currentIndexMap);
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
                StartCoroutine(SetupAsync());
                process.IsShowHint = false;
                udc.SetData(UserDataKeys.USER_PROGRESSION, process);
            }
            else if(process.IsShowSwipe)
            {
                process.IsShowSwipe = false;
                gamePlayUI.ShowUISwipe(true);
                udc.SetData(UserDataKeys.USER_PROGRESSION, process);
            }
        }
    }

    IEnumerator SetupAsync()
    {
        yield return new WaitForSeconds(0.1f);
        Transform s = gamePlayUI.GetIconPeopleManagerUI().Icons[1].transform;
        var list = gamePlayUI.GetIconPeopleManagerUI().Icons[1].Targets;
        Transform e = list[Random.Range(0, list.Count)].transform;
        gamePlayUI.SetHint(s, e);
    }

    public void SpawnLevel() {
        StartCoroutine(SpawnLevelAsync());
    }

    public void ResumeCamera(Action OnComplete = null)
    {
        StartCoroutine(ResumeCameraAsync(OnComplete));
    }

    private IEnumerator ResumeCameraAsync(Action OnComplete = null)
    {
        camera.ReturnVirtualCameraToOrigin();
        yield return new WaitUntil(() => {
            return camera.state == CameraManager.StateVirtualCamera.Finish;
        });
        
        OnComplete?.Invoke();
    }

    private IEnumerator SpawnLevelAsync() {
        if (currentMapManager) {
            gamePlayUI.GetIconHomeManagerUI().FinishMap();
            gamePlayUI.GetIconPeopleManagerUI().FinishMap();
            currentMapManager.DeleteAllCats();
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
        gamePlayUI.UpdateTitle(indexMapManager, currentMapManager.currentIndexMap);

        LoadHealth();
        yield return null;
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
        
        currentMapManager.InitData();
        var element = camera.GetVirtualCameraFree(currentMapManager.GetCurrentCameraPosition());
        camera.MoveCameraToVirtualCamera(element, SetUpTutorial);
    }

    private void SetUpActionCurrentMapManager() {
        currentMapManager.OnFinishLevel += gamePlayUI.ShowUIWin;
        currentMapManager.OnFinishLevel += () => {
            currentMapManager.currentIndexMap++;
            if (currentMapManager.currentIndexMap >= currentMapManager.GetCountMaps()) {
                currentMapManager.RemoveCurrentNavmeshData();
                indexMapManager++;
                Debug.Log($"steps:{dataLevelsSO.CountSteps()} - lvls: {dataLevelsSO.CountLevel()} - currentLvl: {indexMapManager}");
                if (indexMapManager >= dataLevelsSO.CountLevel()) indexMapManager = 0;
                var pro = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
                pro.currentLevel = indexMapManager;
                udc.SetData(UserDataKeys.USER_PROGRESSION, pro);
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
            SetUpTutorial();
        };

        currentMapManager.OnMapBusy += gamePlayUI.EnableRaycastTargetIconPeople;
        currentMapManager.OnCorrect += gamePlayUI.SetSmoothBar;
    }


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

    [ContextMenu("DeleteData")]
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    [ContextMenu("SetData")]
    public void SetData()
    {
        var pro = UserDataController.Instance.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        pro.currentLevel = indexMapManager;
        UserDataController.Instance.SetData(UserDataKeys.USER_PROGRESSION, pro);
    }

}
