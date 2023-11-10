using System;
using UnityEngine;
using IronPirate.Api.Ads;

namespace IronPirate {
    [RequireComponent(typeof(RectTransform))]
    public class BannerAdListener : MonoBehaviour {

        RectTransform myRect;
        float heightRatio = 1;

        private void Start() {
            AdsManager.Instance.onBannerHeightChanged += OnBannerHeightChanged;
            myRect = GetComponent<RectTransform>();
            GetComponent<UnityEngine.UI.Image>().color = Color.white;

            var scaler = GetComponentInParent<UnityEngine.UI.CanvasScaler>();
            if (scaler) heightRatio = scaler.referenceResolution.y / 1280f;

            OnBannerHeightChanged();
        }

        private void OnDestroy() {
            if (AdsManager.Initialized) AdsManager.Instance.onBannerHeightChanged -= OnBannerHeightChanged;
        }

        private void OnBannerHeightChanged() {
            Logs.Log($"OnBannerHeightChanged height={AdsManager.Instance.GetBannerHeight()}");
            myRect.sizeDelta = new Vector2(myRect.sizeDelta.x, AdsManager.Instance.GetBannerHeight() * heightRatio);
        }
    }
}
