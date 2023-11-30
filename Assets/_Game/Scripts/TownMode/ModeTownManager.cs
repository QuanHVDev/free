using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class ModeTownManager : SingletonBehaviour<ModeTownManager>
{
    [SerializeField] private List<Transform> transformSpawnTownManagers;
    [SerializeField] private LayerMask houseLayerMask;
    [SerializeField] private LayerMask townLayerMask;    
    [SerializeField] private LayerMask backUIAdoptLayerMask;


    private GameManager.StateTouch touchState;
    private StateMode state;
    private int currentSelectTown, currentIndexHouse;
    private SingleTownManager[] townManagersSpawned;
    private SingleTownManager currentSingleTownManager;
    private ModeTownUI modeTownUI;
    private TownLevelsSO townLevels;

    private enum StateMode
    {
        Free,
        Busy
    }

    private void Start()
    {
        OnOutModeTown += () =>
        {
            currentIndexHouse = -1;
        };

        OnInModeTown += () =>
        {

        };
    }

    public void Init()
    {
        Input.multiTouchEnabled = false;
        
        modeTownUI = UIRoot.Ins.Get<ModeTownUI>();
        modeTownUI.Init();
        InitSelectionCat();
        townLevels = GameSettings.Ins.TownsDataSO;
        
        townManagersSpawned = new SingleTownManager[townLevels.Data.Count];
        currentSelectTown = 0;
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        OnAfterDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        SpawnTown();
    }

    private void InitSelectionCat()
    {
        var processTown =
            UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);

        List<PeopleSO> data = new List<PeopleSO>();
        foreach (var id in processTown.catSelectionDatas)
        {
            var catSO = GameSettings.Ins.GetCatSO(id);
            if (catSO) data.Add(catSO);
            else
                Debug.Log($"NULL with id: {id}");
        }
        
        modeTownUI.InitSelectionCat(data);
    }

    private void SpawnTown()
    {
        int startIndex = Math.Clamp(currentSelectTown - 2, 0, townLevels.Data.Count - 1);
        int endIndex = Math.Clamp(currentSelectTown + 2, 0, townLevels.Data.Count - 1);
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (townManagersSpawned[i] == null)
            {
                townManagersSpawned[i] = Instantiate(townLevels.Data[i].SingleTownManager, transformSpawnTownManagers[i]);
            }
        }
    }

    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                touchState = GameManager.StateTouch.free;
                return;
            }

            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                touchState = GameManager.StateTouch.touchUI;
                return;
            }

            if (touchState != GameManager.StateTouch.free) return;
            Touch touch = Input.GetTouch(0);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            switch (state)
            {
                case StateMode.Free:
                    if (Physics.Raycast(ray, out hit, 999f, townLayerMask))
                    {
                        if (hit.transform.TryGetComponent(out SingleTownManager townManager))
                        {
                            state = StateMode.Busy;
                            CameraMainMenu.Instance.InModeTown();
                            townManager.Init();
                            this.currentSingleTownManager = townManager;
                            ShowNotiHouse();
                            OnInModeTown?.Invoke();
                        }
                    }

                    break;
                case StateMode.Busy:
                    if (Physics.Raycast(ray, out hit, 999f, houseLayerMask))
                    {
                        if (hit.transform.TryGetComponent(out House house))
                        {
                            currentIndexHouse = townManagersSpawned[currentSelectTown].GetHouses().IndexOf(house);
                            if (currentIndexHouse < 0)
                            {
                                Debug.Log("Null house");
                                return;
                            }
                            
                            touchState = GameManager.StateTouch.touchObject;
                            house.Interact();
                        }
                    }
                    else if (Physics.Raycast(ray, out hit, 999f, backUIAdoptLayerMask))
                    {
                        modeTownUI.HideAdoptUI();
                    }

                    break;
            }
        }
    }
    
    public void ShowNotiHouse()
    {
        var p = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        
        //Tìm data của Town đang select
        List<string> dataTown = new List<string>();
        foreach (var query in p.catSelectedDatas)
        {
            query.StartsWith(currentSelectTown.ToString());
            dataTown.Add(query);
        }
        
        // duyệt từng house có trong Town, nếu house nào đã đủ mèo thì sẽ tắt noti
        List<int> dataSearched = new List<int>();
        foreach (var query in dataTown)
        {
            var arr = query.Split('|');
            int indexHouse = -1;
            
            if (!int.TryParse(arr[1], out indexHouse) ) {
                Debug.Log("Convert fail");
                continue;
            }

            if (dataSearched.Contains(indexHouse))
            {
                continue;
            }
            
            dataSearched.Add(indexHouse);
            int count = currentSingleTownManager.GetHouses()[indexHouse].CountTagCats();
            string text = $"{currentSelectTown}|{indexHouse}";
            foreach (var data in dataTown)
            {
                if (data.StartsWith(text))
                {
                    count--;
                }
            }

            Debug.Log($"{currentSingleTownManager} - house:{indexHouse} - count:{count}");
            currentSingleTownManager.GetHouses()[indexHouse].EnableNoti(count > 0);
        }

        // những house không có trong data thì sẽ chưa được duyệt nên bật noti lên
        for (int i = 0; i < currentSingleTownManager.GetHouses().Count; i++)
        {
            if (!dataSearched.Contains(i))
            {
                currentSingleTownManager.GetHouses()[i].EnableNoti(true);
            }
        }
    }

    public void OutModeTown()
    {
        state = StateMode.Free;
        currentSingleTownManager.Out();
        CameraMainMenu.Instance.OutModeTown();
        OnOutModeTown?.Invoke();
    }

    public Action OnOutModeTown, OnInModeTown;
    public Action<int, int> OnAfterDoMoveIsland, OnBeforeDoMoveIsland;
    public void BackToBeforeTown()
    {
        modeTownUI.HideAdoptUI();
        
        currentSelectTown = Mathf.Clamp(--currentSelectTown, 0, townLevels.Data.Count - 1);
        this.currentSingleTownManager.Out();
        this.currentSingleTownManager = townManagersSpawned[currentSelectTown];
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        transform.DOMoveX(transform.position.x + 120, 1f).OnComplete(() =>
        {
            OnAfterDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
            ShowNotiHouse();
        });
        
        SpawnTown();
    }
    public void NextToAfterTown()
    {
        modeTownUI.HideAdoptUI();
        
        currentSelectTown = Mathf.Clamp(++currentSelectTown, 0, townLevels.Data.Count - 1);
        this.currentSingleTownManager.Out();
        this.currentSingleTownManager = townManagersSpawned[currentSelectTown];
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        transform.DOMoveX(transform.position.x - 120, 1f).OnComplete(() =>
        {
            if (currentSelectTown < townLevels.Data.Count)
                OnAfterDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
            ShowNotiHouse();
        });
        SpawnTown();
    }

    private int amountCat = 3;
    public void AddCatTown()
    {
        List<PeopleSO> peoples = new List<PeopleSO>();
        
        var data = new List<PeopleSO>(GameSettings.Ins.CatsDataSO.ListData);
        for (int i = 0; i < amountCat; i++)
        {
            var dataRandom = data[Random.Range(0, data.Count)];
            data.Remove(dataRandom);
            peoples.Add(dataRandom);
        }

        modeTownUI.InitAddCat(peoples);
    }

    public void SelectOption(PeopleSO data, bool isAds)
    {
        if (isAds)
        {
            Debug.Log("TODO: SHOWADS");
        }
        
        var processTown =
            UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        processTown.catSelectionDatas.Add(data.id);
        UserDataController.Instance.SetData(UserDataKeys.USER_PROGRESSION_MODETOWN, processTown);
        
        modeTownUI.AddSelectionCat(data);
        modeTownUI.HideGetCatUI();
    }

    public IconCatSelected SetSelection(PeopleSO data)
    {
        modeTownUI.EnableVerticalScroll(false);
        return modeTownUI.SetSelected(data);
    }

    public void ShowRequestHouse(List<TagCat> tagCats, House house)
    {
        modeTownUI.ShowRequestHouse(tagCats, house);
        var p = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        foreach (var query in p.catSelectedDatas)
        {
            var arr = query.Split('|');
            int indexTown = -1, indexHouse = -1, indexTag = -1;
            var data = GameSettings.Ins.GetCatSO(arr[3]);
            if (int.TryParse(arr[0], out indexTown) && int.TryParse(arr[1], out indexHouse) &&
                int.TryParse(arr[2], out indexTag))
            {
                if (indexTown == currentSelectTown && indexHouse == currentIndexHouse && data)
                {
                    modeTownUI.ShowFilledAdopt(indexTag, data);
                }
            }
            else
            {
                Debug.Log($"Convert NULL: {query}");
            }
        }
    }

    public bool SaveCorrect(int indexTag, PeopleSO dataCat)
    {
        var processTown =
            UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);

        if (!processTown.catSelectionDatas.Remove(dataCat.id))
        {
            Debug.Log($"NOT OWNER {dataCat.id}");
            return false;
        }

        // town {index} | house {index} | Tag {index} | Cat {id}
        // t{X}h{Y}t{Z}c{Q}

        string query = $"{currentSelectTown}|" 
                       + $"{townManagersSpawned[currentSelectTown].GetHouses()[currentIndexHouse].Query}|"
                       + $"{indexTag}|"
                       + $"{dataCat.id}";
        
        Debug.Log($"query: {query}");
        processTown.catSelectedDatas.Add(query);
        UserDataController.Instance.SetData(UserDataKeys.USER_PROGRESSION_MODETOWN, processTown);
        modeTownUI.ShowFilledAdopt(indexTag, dataCat);
        return true;
    }
}