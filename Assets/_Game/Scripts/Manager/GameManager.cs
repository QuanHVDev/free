using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
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
        Input.multiTouchEnabled = false;
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

    private Touch touch;
    private Ray ray;
    private RaycastHit hit;
    
    [SerializeField] private LayerMask mouseColliderLayerMark;
    public StateTouch IsTouchUI = StateTouch.free;

    public enum StateTouch
    {
        free,
        touchUI,
        touchRotate,
        touchObject
    }
    
    private void Update()
    {
        Debug.Log(IsTouchUI);
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                IsTouchUI = StateTouch.free;
                return;
            }
            
            if (IsTouchUI != StateTouch.free) return;
            
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                IsTouchUI = StateTouch.touchUI;
            }

            if (currentMapManager && !currentMapManager.IsMapBusy)
            {
                Debug.Log(" ray");
                touch = Input.GetTouch(0);
                ray = UnityEngine.Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, 999f, mouseColliderLayerMark))
                {
                    if (hit.transform.TryGetComponent(out Condition condition))
                    {
                        IsTouchUI = StateTouch.touchObject;
                        condition.ClickObject();
                    }
                }
            }
        }
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
            }else if (process.IsShowSkip)
            {
                Time.timeScale = 0.7f;
                gamePlayUI.ShowHintObj(gamePlayUI.BtnSkip.transform, () =>
                {
                    Time.timeScale = 1f;
                    DoSkip();
                    gamePlayUI.ShowButtonSkip(false);
                });
                process.IsShowSkip = false;
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
            if (currentMapManager.currentIndexMap >= currentMapManager.GetCountMaps())
            {
                yield return ResumeCameraAsync(() =>
                {
                    Destroy(currentMapManager.gameObject);
                });
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
            if (currentMapManager.CountCatMoved < currentMapManager.MaxCatNeedMove)
            {
                gamePlayUI.ShowButtonSkip(true);
            }
            SetUpTutorial();
        };
        currentMapManager.OnCompletePath += () => {
            var element = camera.GetElementCameraPrev();
            camera.ChangeState(element.triggerNameAnimationState, CameraManager.StateVirtualCamera.Wait);
            element.VirtualCamera.gameObject.SetActive(true);
            SetUpTutorial();
            gamePlayUI.ShowButtonSkip(false);
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
    
    public bool IsSkip { get; private set; }
    public void ResetSkip()
    {
        IsSkip = false;
    }

    public void DoSkip()
    {
        IsSkip = true;
    }
}
