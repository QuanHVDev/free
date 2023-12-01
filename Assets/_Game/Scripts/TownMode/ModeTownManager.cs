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
        WaitToBusy,
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
        
        var processTown =
            UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        currentSelectTown = processTown.indexProcessingTown;
        transform.DOMoveX(transform.position.x - currentSelectTown * 120, 1f);
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        OnAfterDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        modeTownUI.SetTextTownName(townLevels.Data[currentSelectTown].nameTown);

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
        modeTownUI.SetTextCoutSelection(processTown.catSelectionDatas.Count);
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
                        var processTownFree = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
                        if (hit.transform.TryGetComponent(out SingleTownManager townManager))
                        {
                            state = StateMode.WaitToBusy;
                            CameraMainMenu.Instance.InModeTown();
                            this.currentSingleTownManager = townManager;
                            
                            OnInModeTown?.Invoke();
                            if (currentSelectTown == processTownFree.indexProcessingTown)
                            {
                                ShowNotiHouse();
                            }

                            StartCoroutine(WaitToBusyState());
                        }
                    }
                    break;
                
                case StateMode.Busy:
                    if (Physics.Raycast(ray, out hit, 999f, houseLayerMask))
                    {
                        var processTownBusy = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
                        if (currentSelectTown > processTownBusy.indexProcessingTown)
                        {
                            return;
                        }
                        
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

    private IEnumerator WaitToBusyState()
    {
        yield return new WaitUntil(() => Input.touchCount == 0);
        state = StateMode.Busy;
        SetValueProcessBar();
    }
    
    public void ShowNotiHouse()
    {
        for (int i = 0; i < currentSingleTownManager.GetHouses().Count; i++)
        {
            currentSingleTownManager.GetHouses()[i].EnableNoti(IsActiveNotiHouseinCurrentTownWithText(i));
        }

        SetValueProcessBar();
    }

    private void SetValueProcessBar()
    {
        int count = 0;
        for (int i = 0; i < currentSingleTownManager.GetHouses().Count; i++)
        {
            bool isActive = IsActiveNotiHouseinCurrentTownWithText(i);
            if (!isActive) count++;
        }
        
        modeTownUI.SetValueForProcessBar(count * 1.0f / currentSingleTownManager.GetHouses().Count);
    }

    private bool IsActiveNotiHouseinCurrentTownWithText(int indexHouseCheck)
    {
        int count = 0;
        string text = $"{currentSelectTown}|{indexHouseCheck}";
        var modeTownData = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        foreach (var data in modeTownData.catSelectedDatas)
        {
            if (data.StartsWith(text))
            {
                count++;
            }
        }

        return currentSingleTownManager.GetHouses()[indexHouseCheck].CountTagCats() - count > 0;
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
        modeTownUI.HideProcessBar();
        
        currentSelectTown = Mathf.Clamp(--currentSelectTown, 0, townLevels.Data.Count - 1);
        this.currentSingleTownManager.Out();
        this.currentSingleTownManager = townManagersSpawned[currentSelectTown];
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        transform.DOMoveX(transform.position.x + 120, 1f).OnComplete(() =>
        {
            modeTownUI.SetTextTownName(townLevels.Data[currentSelectTown].nameTown);
            OnAfterDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
            var processTown = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
            if (currentSelectTown == processTown.indexProcessingTown)
            {
                ShowNotiHouse();
            }

            SetValueProcessBar();
        });
        
        SpawnTown();
    }
    public void NextToAfterTown()
    {
        modeTownUI.HideAdoptUI();
        modeTownUI.HideProcessBar();
        
        currentSelectTown = Mathf.Clamp(++currentSelectTown, 0, townLevels.Data.Count - 1);
        this.currentSingleTownManager.Out();
        this.currentSingleTownManager = townManagersSpawned[currentSelectTown];
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
        transform.DOMoveX(transform.position.x - 120, 1f).OnComplete(() =>
        {
            modeTownUI.SetTextTownName(townLevels.Data[currentSelectTown].nameTown);
            if (currentSelectTown < townLevels.Data.Count)
                OnAfterDoMoveIsland?.Invoke(currentSelectTown, townLevels.Data.Count - 1);
            var processTown = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
            if (currentSelectTown == processTown.indexProcessingTown)
            {
                ShowNotiHouse();
            }
            
            SetValueProcessBar();
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
        modeTownUI.SetTextCoutSelection(processTown.catSelectionDatas.Count);
        modeTownUI.HideAddCatUI();
    }

    public IconCatSelected SetSelection(PeopleSO data)
    {
        modeTownUI.EnableVerticalScroll(false);
        return modeTownUI.SetSelected(data);
    }

    public void ShowRequestHouse(List<TagCat> tagCats, House house)
    {
        var p = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        modeTownUI.ShowRequestHouse(tagCats, house);
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
                    modeTownUI.ShowFilledAdopt(indexTag, data, p.indexProcessingTown == currentSelectTown);
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
                       + $"{currentIndexHouse}|"
                       + $"{indexTag}|"
                       + $"{dataCat.id}";
        
        Debug.Log($"query: {query}");
        processTown.catSelectedDatas.Add(query);
        modeTownUI.ShowFilledAdopt(indexTag, dataCat, processTown.indexProcessingTown == currentSelectTown);
        UserDataController.Instance.SetData(UserDataKeys.USER_PROGRESSION_MODETOWN, processTown);

        if (!IsActiveNotiHouseinCurrentTownWithText(currentIndexHouse))
        {
            currentSingleTownManager.GetHouses()[currentIndexHouse].EnableNoti(false);
            modeTownUI.HideAdoptUI();
            if (IsCompleteProcessTown(processTown.indexProcessingTown))
            {
                processTown.indexProcessingTown++;
                UserDataController.Instance.SetData(UserDataKeys.USER_PROGRESSION_MODETOWN, processTown);
                SFX.Instance.PlayCompleteTown();
            }
            else
            {
                SFX.Instance.PlayCorrect();
            }
        }
        
        SetValueProcessBar();
        modeTownUI.SetTextCoutSelection(processTown.catSelectionDatas.Count);
        return true;
    }


    private bool IsCompleteProcessTown(int index)
    {
        int countProcessNeed = 0;
        foreach (var house in townLevels.Data[index].SingleTownManager.GetHouses())
        {
            countProcessNeed += house.CountTagCats();
        }

        var processTown =
            UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        foreach (var query in processTown.catSelectedDatas)
        {
            if (query.StartsWith($"{index}|"))
            {
                countProcessNeed--;
            }
        }
        
        return countProcessNeed <= 0;
    }

    public bool UndoCatSelected(int indexTag, PeopleSO data)
    {
        var processTown = UserDataController.Instance.GetData<ProcessModeTown>(UserDataKeys.USER_PROGRESSION_MODETOWN, out _);
        if (processTown.indexProcessingTown != currentSelectTown)
        {
            Debug.Log($"Not undo with {currentSelectTown}");
            return false;
        }

        string query = $"{currentSelectTown}|{currentIndexHouse}|{indexTag}|{data.id}";
        if (!processTown.catSelectedDatas.Remove(query))
        {
            Debug.Log($"Not undo with {currentSelectTown}");
            return false;
        }
        
        Debug.Log($"Undo: {query}");
        processTown.catSelectionDatas.Add(data.id);
        UserDataController.Instance.SetData(UserDataKeys.USER_PROGRESSION_MODETOWN, processTown);
        modeTownUI.AddSelectionCat(data);
        currentSingleTownManager.GetHouses()[currentIndexHouse].EnableNoti(true);
        SetValueProcessBar();
        modeTownUI.SetTextCoutSelection(processTown.catSelectionDatas.Count);
        return true;
    }
}