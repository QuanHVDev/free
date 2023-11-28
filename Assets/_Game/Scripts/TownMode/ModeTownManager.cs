using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class ModeTownManager : SingletonBehaviour<ModeTownManager>
{
    [SerializeField] private List<SingleTownManager> townManagerDatas;
    [SerializeField] private List<Transform> transformSpawnTownManagers;
    [SerializeField] private LayerMask houseLayerMask;
    [SerializeField] private LayerMask townLayerMask;    
    [SerializeField] private LayerMask backUIAdoptLayerMask;


    private GameManager.StateTouch touchState;
    private StateMode state;
    private int currentSelectTown;
    private SingleTownManager[] _townManagersSpawned;
    private SingleTownManager currentSingleTownManager;
    private ModeTownUI modeTownUI;

    private enum StateMode
    {
        Free,
        Busy
    }

    public void Init()
    {
        Input.multiTouchEnabled = false;
        
        modeTownUI = UIRoot.Ins.Get<ModeTownUI>();
        modeTownUI.Init();
        modeTownUI.InitSelectionCat(new List<PeopleSO>());
        
        _townManagersSpawned = new SingleTownManager[townManagerDatas.Count];
        currentSelectTown = 0;
        
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townManagerDatas.Count - 1);
        OnAfterDoMoveIsland?.Invoke(currentSelectTown, townManagerDatas.Count - 1);
        SpawnTown();
    }

    private void SpawnTown()
    {
        int startIndex = Math.Clamp(currentSelectTown - 2, 0, townManagerDatas.Count - 1);
        int endIndex = Math.Clamp(currentSelectTown + 2, 0, townManagerDatas.Count - 1);
        for (int i = startIndex; i <= endIndex; i++)
        {
            if (_townManagersSpawned[i] == null)
            {
                _townManagersSpawned[i] = Instantiate(townManagerDatas[i], transformSpawnTownManagers[i]);
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
                            OnInModeTown?.Invoke();
                        }
                    }

                    break;
                case StateMode.Busy:
                    if (Physics.Raycast(ray, out hit, 999f, houseLayerMask))
                    {
                        if (hit.transform.TryGetComponent(out House house))
                        {
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
        currentSelectTown = Mathf.Clamp(--currentSelectTown, 0, townManagerDatas.Count - 1);
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townManagerDatas.Count - 1);
        transform.DOMoveX(transform.position.x + 120, 1f).OnComplete(() =>
        {
            OnAfterDoMoveIsland?.Invoke(currentSelectTown, townManagerDatas.Count - 1);
        });
        SpawnTown();
    }
    public void NextToAfterTown()
    {
        currentSelectTown = Mathf.Clamp(++currentSelectTown, 0, townManagerDatas.Count - 1);
        OnBeforeDoMoveIsland?.Invoke(currentSelectTown, townManagerDatas.Count - 1);
        transform.DOMoveX(transform.position.x - 120, 1f).OnComplete(() =>
        {
            if (currentSelectTown < townManagerDatas.Count)
                OnAfterDoMoveIsland?.Invoke(currentSelectTown, townManagerDatas.Count - 1);
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
        
        modeTownUI.AddSelectionCat(data);
        modeTownUI.HideGetCatUI();
    }

    public IconCatSelected SetSelection(PeopleSO data)
    {
        modeTownUI.EnableVerticalScroll(false);
        return modeTownUI.SetSelected(data, ()=>
        {
            modeTownUI.EnableVerticalScroll(true);
        });
    }

    public void ShowRequestHouse(List<TagCat> tagCats, House house)
    {
        modeTownUI.ShowRequestHouse(tagCats, house);
    }
}