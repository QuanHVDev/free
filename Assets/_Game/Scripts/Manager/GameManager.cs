using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GameManager : SingletonBehaviour<GameManager> {
    [SerializeField] private GamePlayUI gamePlayUI;
    [SerializeField] private Transform spawnMap;
    
    [SerializeField] private DataLevelsSO dataLevelsSO;
    [SerializeField] private int indexMapManager;
    [SerializeField] private CameraManager camera;
    
    private MapManager currentMapManager;
    public MapManager CurrentMapManager => currentMapManager;
    public int IndexMapManager => indexMapManager;
    
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
            SpawnLevel(pro.currentLevel, pro.currentStep);
        }

        gamePlayUI.UpdateTitle();
        gamePlayUI.InitListLevel(dataLevelsSO);
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
        //Debug.Log(IsTouchUI);
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
               // Debug.Log(" ray");
                touch = Input.GetTouch(0);
                ray = UnityEngine.Camera.main.ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, 999f, mouseColliderLayerMark))
                {
                    if (hit.transform.TryGetComponent(out Condition condition) && !condition.IsCanShow)
                    {
                        //Debug.Log(" ray to" + hit.transform.name);
                        IsTouchUI = StateTouch.touchObject;
                    }
                }

                mousePosition = touch.position;
                mousePosition.z = 100;
                mousePosition = UnityEngine.Camera.main.ScreenToWorldPoint(mousePosition);
            }
        }
    }

    private void LateUpdate()
    {
        Debug.DrawRay(
            UnityEngine.Camera.main.transform.position, 
            mousePosition - UnityEngine.Camera.main.transform.position,
            Color.blue);
    }

    private Vector3 mousePosition;

    private void SetUpTutorial()
    {
        var process = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        if (!process.IsShowSwipe && !process.IsShowHint && !process.IsShowSkip)
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

    public void NextLevel()
    {
        var pro = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        pro.currentLevel = indexMapManager;
        pro.currentStep = currentMapManager.currentIndexStep;
        udc.SetData(UserDataKeys.USER_PROGRESSION, pro);
        SpawnLevel(pro.currentLevel, pro.currentStep);
    }

    public void SpawnLevel(int level , int step)
    {
        StartCoroutine(SpawnLevelAsync(level, step));
    }

    private IEnumerator SpawnLevelAsync(int level = -1, int step = -1)
    {
        if (currentMapManager)
        {
            yield return RemovePreMapAsync(level != indexMapManager);
        }
        
        LoadLevel(level, step);
        LoadLevelUI();
        LoadHealth();
        yield return null;
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

    private IEnumerator RemovePreMapAsync(bool isForceDestroyMap)
    {
        gamePlayUI.GetIconHomeManagerUI().FinishMap();
        gamePlayUI.GetIconPeopleManagerUI().FinishMap();
        currentMapManager.DeleteAllCats();
        if (currentMapManager.currentIndexStep >= currentMapManager.GetCountMaps() || isForceDestroyMap)
        {
            yield return ResumeCameraAsync(() =>
            {
                Destroy(currentMapManager.gameObject);
            });
        }
    }


    private void LoadLevelUI()
    {
        gamePlayUI.GetIconHomeManagerUI().Add(currentMapManager, gamePlayUI);
        gamePlayUI.GetIconPeopleManagerUI().SetAllHomesForIcon(gamePlayUI.GetIconHomeManagerUI().GetCurrentIconForMap());
        gamePlayUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());
        gamePlayUI.UpdateTitle();
    }

    private void LoadLevel(int level, int step = -1) {
        if (indexMapManager != level || !currentMapManager || currentMapManager.currentIndexStep >= currentMapManager.GetCountMaps()) {
            indexMapManager = level;
            currentMapManager = Instantiate(dataLevelsSO.MapManagers[indexMapManager], spawnMap);
            SetUpActionCurrentMapManager();
        }
        
        currentMapManager.InitData(step);
        var element = camera.GetVirtualCameraFree(currentMapManager.GetCurrentCameraPosition());
        camera.MoveCameraToVirtualCamera(element, SetUpTutorial);
    }

    private void SetUpActionCurrentMapManager() {
        currentMapManager.OnFinishLevel += () =>
        {
            gamePlayUI.ShowUIWin();
            currentMapManager.currentIndexStep++;
            if (currentMapManager.currentIndexStep >= currentMapManager.GetCountMaps()) {
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
