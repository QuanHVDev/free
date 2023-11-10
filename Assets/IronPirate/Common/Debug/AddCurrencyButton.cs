using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace IronPirate {
    [RequireComponent(typeof(Button))]
    public class AddCurrencyButton : MonoBehaviour {
        private void Start() {
            if (!Logs.CheatEnable) DestroyImmediate(gameObject);
            else GetComponent<Button>().onClick.AddListener(OnButtonClicked);
        }

        private void OnButtonClicked() {
            PrefData.SetCash(PrefData.CurrentCash + 100000);
        }
    }
}