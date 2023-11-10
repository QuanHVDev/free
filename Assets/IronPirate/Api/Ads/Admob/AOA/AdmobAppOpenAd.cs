//#if ADMOB_AOA
using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

namespace IronPirate.Api.Ads {
    public class AdmobAppOpenAd {
        private static AdmobAppOpenAd instance;
        public static AdmobAppOpenAd Instance {
            get {
                if (instance == null) {
                    instance = new AdmobAppOpenAd();
                }

                return instance;
            }
        }

#if UNITY_ANDROID
        private const string ID_TEST_ADS = "ca-app-pub-3940256099942544/3419835294";

#elif UNITY_IOS
        private const string ID_TEST_ADS = "ca-app-pub-3940256099942544/5662855259";
#endif

        private string[] idTiers;
        private bool useTestAd;

        private int tierIndex = 0;

        private AppOpenAd ad;
        public bool HasAOA => ad != null;

        public Action onAdLoaded, onAdFailedToLoad;

        private Action onAdClose;
        public void Init(string[] idTiers, bool useTestAd, Action onAdLoaded, Action onAdFailedToLoad) {
            this.idTiers = idTiers;
            this.useTestAd = useTestAd;
            this.onAdLoaded = onAdLoaded;
            this.onAdFailedToLoad = onAdFailedToLoad;

            LoadAd();
        }

        private void Log(string msg, bool forceLog = false) {
            if (!forceLog) Logs.Log(string.Format("[Ads.Admob.AOA] {0}", msg));
            else Debug.Log(string.Format("[Ads.Admob.AOA] {0}", msg));
        }

        private void LogError(string msg) {
            Debug.LogError(string.Format("[Ads.Admob.AOA] {0}", msg));
        }

        private void OnAdLoadedCallback() {
            Log("OnAdLoaded");
            if (onAdLoaded != null) onAdLoaded.Invoke();
        }

        private void OnAdFailedToLoadCallback() {
            if (onAdFailedToLoad != null) onAdFailedToLoad.Invoke();
        }

        public void LoadAd() {
            // if (IAP_AD_REMOVED)
            //     return;

            LoadAOA();
        }

        public void LoadAOA() {
            string id = string.Empty;
            if (useTestAd) {
                id = ID_TEST_ADS;
            }
            else if (idTiers != null && idTiers.Length > 0) {
                if (tierIndex < idTiers.Length && !string.IsNullOrEmpty(idTiers[tierIndex]))
                    id = idTiers[tierIndex];
                else id = idTiers[0];
            }

            if (string.IsNullOrEmpty(id)) {
                LogError($"Null tier id, tiers = {tierIndex + 1}");
                return;
            }

            Log("Start Request Tier " + (tierIndex + 1), true);

            AdRequest request = new AdRequest();

            AppOpenAd.Load(id, request, OnAdLoaded);
        }

        public void ShowAOA(Action onadClose = null) {
            if (!HasAOA) {
                if (onadClose != null) onadClose.Invoke();
                return;
            }
            this.onAdClose = onadClose;
            ad.OnAdFullScreenContentClosed += OnAdFullScreenContentClosed;
            ad.OnAdFullScreenContentFailed += OnAdFullScreenContentFailed;
            ad.OnAdFullScreenContentOpened += OnAdFullScreenContentOpened;
            ad.OnAdPaid += OnAdPaid;
            ad.Show();
        }

        private void OnAdLoaded(AppOpenAd ad, AdError error)
        {
            if (error != null)
            {
                // Handle the error.
                LogError(string.Format("[AOA] OnAdFailedToLoad err: {0}, tier {1}", error.GetMessage(), tierIndex));
                Excutor.Schedule(() => {
                    Tracking.Instance.LogEvent("ad_aoa_fail", "errormsg", string.Format("loadError: {0}", error.GetMessage()), Tracking.Service.Firebase);
                });
                tierIndex++;
                if (tierIndex >= idTiers.Length) {
                    Excutor.Schedule(OnAdFailedToLoadCallback);
                    tierIndex = 0;
                } else {
                    Excutor.Schedule(LoadAOA);
                }

                return;
            }

            // App open ad is loaded.
            this.ad = ad;
            tierIndex = 0;
            Excutor.Schedule(OnAdLoadedCallback);
            
        }

        private void OnAdFullScreenContentClosed() {
            Log("OnAdFullScreenContentClosed");
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            Excutor.Schedule(onAdClose);
            Excutor.Schedule(LoadAd);
        }

        private void OnAdFullScreenContentFailed(AdError error) {
            LogError(string.Format("OnAdFullScreenContentFailed error= {0}", error.GetMessage()));
            // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
            ad = null;
            Excutor.Schedule(() => {
                Tracking.Instance.LogEvent("ad_aoa_fail", "errormsg", string.Format("showError: {0}", error.GetMessage()), Tracking.Service.Firebase);
                if (onAdClose != null) onAdClose.Invoke();
                LoadAd();
            });
        }

        private void OnAdFullScreenContentOpened() {
            Log("OnAdFullScreenContentOpened");
        }

        private void OnAdPaid(AdValue adValue) {
            Log(string.Format("OnAdPaid. (value: {0}", adValue.Value));
            Excutor.Schedule(() => {
                Tracking.Instance.LogAdImpression(Mediation.admob, "admob", "aoa", "AOA", adValue.CurrencyCode, adValue.Value);
            });
        }
    }
}
//#endif