using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace IronPirate {
    public class FPSCounter : MonoBehaviour {
        [SerializeField] private TMPro.TextMeshProUGUI displayFps;
        public float maxFps = 240f;
        private float deltaTime = 0.0f;
        private float fps = 0.0f;
        private string fpsText = "{0} FPS";

        private void Awake() {
            if (!Logs.CheatEnable) DestroyImmediate(gameObject);
        }

        private void Update() {
            if (!displayFps) return;
            deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
            if (deltaTime > 0) {
                fps = (int)(1.0f / deltaTime);
            }

            if (fps < maxFps && fps >= 0) {
                displayFps.text = string.Format(fpsText, fps.ToString());
            }
            else if (fps >= maxFps) {
                displayFps.text = string.Format(fpsText, maxFps.ToString());
            }
            else {
                return;
            }
        }

    }
}