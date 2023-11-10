using System;
using UnityEngine;

namespace IronPirate {

    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class RemoveAdsButton : MonoBehaviour {
        [SerializeField] TMPro.TextMeshProUGUI priceText;
        public UnityEngine.Events.UnityEvent onSuccess;

        private void Start() {
            if (AdsManager.Instance.IsRemovedAds) {
                Hide();
            }
            else {
                gameObject.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(BuyRemoveAds);
                GameEvents.onRemoveAd += Hide;
            }

            UpdatePriceText();

            IAP.Instance.onInitialized += UpdatePriceText;
        }

        private void OnDestroy() {
            GameEvents.onRemoveAd -= Hide;
            if (IAP.Initialized) IAP.Instance.onInitialized -= UpdatePriceText;
        }

        private void UpdatePriceText() {
            if (AdsManager.Instance.IsRemovedAds) {
                Hide();
                return;
            }
            if (priceText == null) return;
            var product = IAP.Instance.GetLocalPrice(IAP.RemoveAdsKey);
            if (product != null) priceText.text = product.localizedPriceString;
            else Debug.LogError($"Product not exist id={IAP.RemoveAdsKey}");
        }

        private void BuyRemoveAds() {
            if (Logs.CheatEnable) OnBuySuccessfully();
            else {
                IAP.Instance.Buy(IAP.RemoveAdsKey, OnBuySuccessfully, OnBuyFailed);
            }
        }

        private void OnBuySuccessfully() {
            AdsManager.Instance.SetRemovedAds();
            NoticeText.Instance.ShowNotice("Successfully");
        }

        private void OnBuyFailed() {
            NoticeText.Instance.ShowNotice("Somethings wrong! Try again.");
        }

        private void Hide() {
            gameObject.SetActive(false);
            if (onSuccess != null) onSuccess.Invoke();
        }


    }

}