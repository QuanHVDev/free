using System;
using UnityEngine;
using IronPirate.Api.Ads;
using GoogleMobileAds.Api;

namespace IronPirate.ReloadRush {
    public class NativeAdView : MonoBehaviour {
        [SerializeField] Texture2D defaultMainTex;
        [SerializeField] MeshRenderer adChoice;
        [SerializeField] MeshRenderer mainTexture;
        [SerializeField] TMPro.TextMeshPro headText;

        private void Start() {
            AdmobNativeAd.Instance.onAdNativeLoaded += DisplayNativeAd;
            DisplayNativeAd();
        }

        private void OnDestroy() {
            if (AdmobNativeAd.Initialized) {
                AdmobNativeAd.Instance.onAdNativeLoaded -= DisplayNativeAd;
            }
        }

        private void DisplayNativeAd() {
            if (mainTexture && defaultMainTex) mainTexture.material.mainTexture = defaultMainTex;
            if (AdmobNativeAd.Instance.CacheNativeAd == null) return;

            var ad = AdmobNativeAd.Instance.CacheNativeAd;
            Debug.Log("[Ads.NativeAdView] DisplayNativeAd..");
            // Show adChoice
            if (adChoice) {
                adChoice.material.mainTexture = ad.GetAdChoicesLogoTexture();
                if (adChoice.gameObject.GetComponent<Collider>() == null) {
                    adChoice.gameObject.AddComponent<BoxCollider>();
                }

                if (!ad.RegisterAdChoicesLogoGameObject(adChoice.gameObject)) {
                    Debug.LogError("[Ads.NativeAdView] Failed: RegisterAdChoicesLogoGameObject");
                }
                else Logs.Log("[Ads.NativeAdView] Registered AdChoices");
            }
            else Debug.LogError("[Ads.NativeAdView] adChoice cannot be null!");

            // Show headline text
            if (headText) {
                headText.SetText(ad.GetHeadlineText());
                if (headText.GetComponent<Collider>() == null) {
                    headText.gameObject.AddComponent<BoxCollider>();
                }
                if (!ad.RegisterHeadlineTextGameObject(headText.gameObject)) {
                    Debug.LogError("[Ads.NativeAdView] Failed: RegisterHeadlineTextGameObject");
                }
                else Logs.Log("[Ads.NativeAdView] Registered HeadlineText");
            }

            // Show main tex
            if (mainTexture) {
                var tex = ad.GetIconTexture();
                if (tex != null) {
                    mainTexture.material.mainTexture = ad.GetIconTexture();

                    if (mainTexture.GetComponent<Collider>() == null) {
                        mainTexture.gameObject.AddComponent<BoxCollider>();
                    }

                    if (!ad.RegisterIconImageGameObject(mainTexture.gameObject)) {
                        Debug.LogError("[Ads.NativeAdView] Failed: RegisterIconImageGameObject");
                    }
                    else Logs.Log("[Ads.NativeAdView] Registered IconImage");
                }
                else if (defaultMainTex) mainTexture.material.mainTexture = defaultMainTex;
            }
        }
    }
}
