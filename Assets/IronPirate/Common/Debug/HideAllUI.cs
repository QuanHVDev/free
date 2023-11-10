using System.Collections.Generic;
using UnityEngine;

public class HideAllUI : MonoBehaviour {
    [SerializeField] int touchCountTrigger = 3;
    [SerializeField] float timeCapping = 1f;
    [SerializeField] GameObject[] uiToHide;

    private float lastTouchedTime;
    private List<GameObject> currentUIs = new List<GameObject>();

    private void Awake() {
        if (!Logs.CheatEnable) DestroyImmediate(gameObject);
    }

    void Update() {
        Touch[] touches = Input.touches;
        if (touches.Length >= touchCountTrigger) {
            if (Time.time - lastTouchedTime >= timeCapping) {
                if (currentUIs.Count <= 0) {
                    foreach (var obj in uiToHide) {
                        if (obj.activeSelf) {
                            currentUIs.Add(obj);
                            obj.SetActive(false);
                        }
                    }
                }
                else {
                    foreach (var UI in currentUIs) {
                        UI.SetActive(true);
                    }
                    currentUIs.Clear();
                }
            }

            lastTouchedTime = Time.time;
        }
    }
}

