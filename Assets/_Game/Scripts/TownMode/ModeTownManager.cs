using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ModeTownManager : SingletonBehaviour<ModeTownManager>
{
    [SerializeField] private List<TownManager> townManagerDatas;
    [SerializeField] private List<Transform> transformSpawnTownManagers;
    [SerializeField] private LayerMask houseLayerMask;
    [SerializeField] private LayerMask townLayerMask;

    private GameManager.StateTouch touchState;
    private StateMode state;
    private int currentSelectTown;
    private TownManager[] _townManagersSpawned;
    private TownManager currentTownManager;
    private TownUI townUI;

    private enum StateMode
    {
        Free,
        Busy
    }

    public void Init()
    {
        townUI = UIRoot.Ins.Get<TownUI>();
        townUI.Init();
        _townManagersSpawned = new TownManager[townManagerDatas.Count];
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
                        if (hit.transform.TryGetComponent(out TownManager townManager))
                        {
                            state = StateMode.Busy;
                            CameraMainMenu.Instance.InModeTown();
                            townManager.Init();
                            this.currentTownManager = townManager;
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

                    break;
            }
        }
    }

    public void OutModeTown()
    {
        state = StateMode.Free;
        currentTownManager.Out();
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
}