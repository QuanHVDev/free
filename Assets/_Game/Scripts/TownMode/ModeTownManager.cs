using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ModeTownManager : MonoBehaviour
{
    [SerializeField] private Camera cameraLookTown;
    [SerializeField] private LayerMask houseLayerMask;
    private GameManager.StateTouch IsTouchUI;
    
    private void Update()
    {
        if (Input.touchCount == 1)
        {
            if (Input.GetMouseButtonUp(0))
            {
                IsTouchUI = GameManager.StateTouch.free;
                return;
            }
            
            if (IsTouchUI != GameManager.StateTouch.free) return;
            
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                IsTouchUI = GameManager.StateTouch.touchUI;
            }

            Touch touch = Input.GetTouch(0);
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, houseLayerMask))
            {
                if (hit.transform.TryGetComponent(out House house))
                {
                    IsTouchUI = GameManager.StateTouch.touchObject;
                    house.Interact();
                }
            }

            Vector3 mousePosition = touch.position;
            mousePosition.z = 100;
            mousePosition = cameraLookTown.ScreenToWorldPoint(mousePosition);
        }
    }
    
}

