using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class ModeFindCatManager : ModeManager
{
    public static ModeFindCatManager Instance { get; private set; }
    
    [FormerlySerializedAs("gamePlayUI")] [SerializeField] private ModeFindCatUI modeFindCatUI;
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

    public override void Init()
    {
        GameManager.Instance.ChangeState(GameState.InMode);
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
            modeFindCatUI.GetIconHomeManagerUI().Add(currentMapManager, modeFindCatUI);
            modeFindCatUI.GetIconPeopleManagerUI()
                .SetAllHomesForIcon(modeFindCatUI.GetIconHomeManagerUI().GetCurrentIconForMap());
            modeFindCatUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());

            LoadHealth();
        }
        else {
            SpawnLevel(pro.currentLevel, pro.currentStep);
        }

        modeFindCatUI.UpdateTitle();
        modeFindCatUI.InitListLevel(dataLevelsSO);
    }

    protected override void Awake()
    {
        Instance = this;
        GameManager.Instance.SetModeManager(this);
    }

    private Touch touch;
    private Ray ray;
    private RaycastHit hit;
    
    [SerializeField] private LayerMask mouseColliderLayerMark;
    public StateTouch TouchUIState = StateTouch.free;

    public enum StateTouch
    {
        free,
        touchUI,
        touchRotate,
        touchObject
    }
    
    private void Update()
    {
        //Debug.Log(TouchUIState);
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                TouchUIState = StateTouch.free;
                return;
            }
            
            if (TouchUIState != StateTouch.free) return;
            
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                TouchUIState = StateTouch.touchUI;
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
                        TouchUIState = StateTouch.touchObject;
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
            modeFindCatUI.TurnOffTutorialUI();
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
                modeFindCatUI.ShowHintObj(modeFindCatUI.BtnSkip.transform, () =>
                {
                    Time.timeScale = 1f;
                    DoSkip();
                    modeFindCatUI.ShowButtonSkip(false);
                });
                process.IsShowSkip = false;
                udc.SetData(UserDataKeys.USER_PROGRESSION, process);
            }
            else if(process.IsShowSwipe)
            {
                process.IsShowSwipe = false;
                modeFindCatUI.ShowUISwipe(true);
                udc.SetData(UserDataKeys.USER_PROGRESSION, process);
            }
        }
    }

    IEnumerator SetupAsync()
    {
        yield return new WaitForSeconds(0.1f);
        Transform s = modeFindCatUI.GetIconPeopleManagerUI().Icons[1].transform;
        var list = modeFindCatUI.GetIconPeopleManagerUI().Icons[1].Targets;
        Transform e = list[Random.Range(0, list.Count)].transform;
        modeFindCatUI.SetHint(s, e);
    }

    public void NextLevel()
    {
        var pro = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
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
        modeFindCatUI.GetIconHomeManagerUI().FinishMap();
        modeFindCatUI.GetIconPeopleManagerUI().FinishMap();
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
        modeFindCatUI.GetIconHomeManagerUI().Add(currentMapManager, modeFindCatUI);
        modeFindCatUI.GetIconPeopleManagerUI().SetAllHomesForIcon(modeFindCatUI.GetIconHomeManagerUI().GetCurrentIconForMap());
        modeFindCatUI.GetMessageManagerUI().Init(currentMapManager.GetMessagesForHint());
        modeFindCatUI.UpdateTitle();
    }

    private void LoadLevel(int level, int step = -1) {
        if (indexMapManager != level || !currentMapManager || currentMapManager.currentIndexStep >= currentMapManager.GetCountMaps()) {
            indexMapManager = level;
            currentMapManager?.RemoveCurrentNavmeshData();
            currentMapManager = Instantiate(dataLevelsSO.MapManagers[indexMapManager], spawnMap);
            SetUpActionCurrentMapManager();
        }
        
        currentMapManager.InitData(step);
        var element = camera.GetVirtualCameraFree(currentMapManager.GetCurrentCameraPosition());
        camera.MoveCameraToVirtualCamera(element, SetUpTutorial);
    }

    private int diamond;
    private void SetUpActionCurrentMapManager() {
        currentMapManager.OnFinishLevel += () =>
        {
            diamond = GameSettings.Ins.diamondPassLevel;
            modeFindCatUI.ShowUIWin();
            modeFindCatUI.AddValueDiamondWinUI(diamond);
            currentMapManager.currentIndexStep++;

            var pro = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
            pro.currentStep = currentMapManager.currentIndexStep;
            
            if (currentMapManager.currentIndexStep >= currentMapManager.GetCountMaps()) {
                currentMapManager.RemoveCurrentNavmeshData();
                indexMapManager++;
                Debug.Log($"steps:{dataLevelsSO.CountSteps()} - lvls: {dataLevelsSO.CountLevel()} - currentLvl: {indexMapManager}");
                if (indexMapManager >= dataLevelsSO.CountLevel()) indexMapManager = 0;
                pro.currentStep = 0;
            }
            
            pro.currentLevel = indexMapManager;
            pro.diamond += diamond;
            RewardUIManager.Instance.AddDiamondNeedShow(diamond);

            udc.SetData(UserDataKeys.USER_PROGRESSION, pro);
        };

        currentMapManager.OnSetCatTarget += camera.GetLookTargetCamera;
        currentMapManager.OnCameraLookTarget += (x, y)=> {
            camera.GetElementCameraPrev().VirtualCamera.gameObject.SetActive(false);
            camera.ChangeState(x, y);
            if (currentMapManager.CountCatMoved < currentMapManager.MaxCatNeedMove)
            {
                modeFindCatUI.ShowButtonSkip(true);
            }
            SetUpTutorial();
        };
        currentMapManager.OnCompletePath += () => {
            var element = camera.GetElementCameraPrev();
            camera.ChangeState(element.triggerNameAnimationState, CameraManager.StateVirtualCamera.Wait);
            element.VirtualCamera.gameObject.SetActive(true);
            SetUpTutorial();
            modeFindCatUI.ShowButtonSkip(false);
        };

        currentMapManager.OnMapBusy += modeFindCatUI.EnableRaycastTargetIconPeople;
        currentMapManager.OnCorrect += modeFindCatUI.SetSmoothBar;
    }


    private void LoadHealth() {
        currentHealth = maxHealth;
        modeFindCatUI.GetIconHealthManagerUI().Init(currentHealth);
    }

    public void Incorrent() {
        --currentHealth;
        modeFindCatUI.GetIconHealthManagerUI().LoseHealth();
    }

    public MapManager GetCurrentMapManager() {
        return currentMapManager;
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

    public void Getx2WithAds()
    {
        Debug.Log("TODO: Show ads");
        modeFindCatUI.AddValueDiamondWinUI(diamond);
        
        var pro = udc.GetData<ProcessData>(UserDataKeys.USER_PROGRESSION, out _);
        pro.diamond += diamond;
        udc.SetData(UserDataKeys.USER_PROGRESSION, pro);
        RewardUIManager.Instance.AddDiamondNeedShow(diamond);

    }
}
