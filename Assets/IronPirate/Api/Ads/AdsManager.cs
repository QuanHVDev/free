using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using IronPirate.Api.Ads;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

namespace IronPirate {

    public class AdsManager : SingletonBehaviourDontDestroy<AdsManager> {

        /// <summary>
        /// Use to listening when the reward video ad is available, usefull for turn on some ad button.
        /// </summary>
        public event Action onRewardVideoAvailable;
        public event Action onInterAvailable;
        public event Action onBannerHeightChanged;

        #region Remove Ads
        private string RemoveAdsKey => IAP.RemoveAdsKey;//$"{UnityEngine.Application.identifier}.removeads";
        /// <summary>
        /// If true, User no longer to see Banner or Interstitial ads, but still can see Reward video ads.
        /// </summary>
        public bool IsRemovedAds {
            get {
                if (hasRemoveAd.HasValue) return hasRemoveAd.Value;
                return (IAP.Initialized && IAP.Instance.IsRemovedAds) || (Logs.CheatEnable && PlayerPrefs.HasKey(RemoveAdsKey));
            }
        }

        private bool? hasRemoveAd;

        /// <summary>
        /// Remove all Banner & Interstital ads (Still keep reward video ads)
        /// <para>Call this when user buy Remove_Ads.</para>
        /// </summary>
        public void SetRemovedAds() {
            Debug.Log("[Ads] You have No-ad!");
            hasRemoveAd = true;
            if (Logs.CheatEnable) PlayerPrefs.SetInt(RemoveAdsKey, 1);
            AdSettings.Ins.UseBannerAd = false;
            AdSettings.Ins.UseInterAd = false;
            AdSettings.Ins.UseAOAAd = false;
            DestroyBanner();
            GameEvents.Trigger(GameEvents.onRemoveAd);
        }

        private void RestoreRemoveAd() {
            if (!IAP.Initialized) return;
            if (IAP.Instance.IsRemovedAds) {
                SetRemovedAds();
            }
            else hasRemoveAd = false;
        }
        #endregion

        private float lastInterShowAtTime;

        protected override void OnAwake() {
            var settings = AdSettings.Ins;
            if (settings == null) return;

            if (Logs.ForMarketing) {
                // Turn off all ad for marketing version
                settings.UseAOAAd = false;
                settings.UseNativeAd = false;
                settings.UseInterAd = false;
                settings.UseBannerAd = false;
                settings.UseRewardAd = false;
                SetRemovedAds();
                return;
            }

            Debug.Log($"[Ads] Initializing... log={Logs.IsEnable}, useTest={settings.UseTestAd}");

            InitAdmob(settings);

            lastInterShowAtTime = - settings.InterCapping;

            RemoteConfig.Instance.AddDefault("ad_inter_capping", settings.InterCapping);
            RemoteConfig.Instance.GetLongAsync("ad_inter_capping", (value) => { 
                settings.InterCapping = value;
                lastInterShowAtTime = -settings.InterCapping;
            });

            RemoteConfig.Instance.GetBoolAsync("ad_inter_resume", (value) => settings.InterResumeEnable = value);

            AdsIronSource.Instance.onRewardVideoAdLoadedHandler += OnRewardVideoAvailable;
            AdsIronSource.Instance.onInterstitialAdLoadedHandler += OnInterAvailable;
            AdsIronSource.Instance.onRewardVideoAdDisplayedHandler += OnRewardVideoDisplayed;
            AdsIronSource.Instance.onInterstitialAdDisplayedHandler += OnInterDisplayed;
            AdsIronSource.Instance.onBannerHeightChanged += OnBannerHeightChanged;
            AdsIronSource.Instance.onAdPaidRevenue += OnAdPaidRevenue;
            AdsIronSource.Instance.onRewardFailedToLoad += OnRewardAdLoadFailed;
            AdsIronSource.Instance.onInterFailedToLoad += OnInterAdLoadFailed;
            AdsIronSource.Instance.onRewardFailedToShow += OnRewardAdShowFailed;
            AdsIronSource.Instance.onInterFailedToShow += OnInterAdShowFailed;

            RestoreRemoveAd();
        }

        private void Start() {
            IAP.Instance.onInitialized += RestoreRemoveAd;
        }

        protected void OnDestroy() {
            if (IAP.Initialized) IAP.Instance.onInitialized -= RestoreRemoveAd;

            if (!AdsIronSource.Initialized) return;
            AdsIronSource.Instance.onRewardVideoAdLoadedHandler -= OnRewardVideoAvailable;
            AdsIronSource.Instance.onInterstitialAdLoadedHandler -= OnInterAvailable;
            AdsIronSource.Instance.onRewardVideoAdDisplayedHandler -= OnRewardVideoDisplayed;
            AdsIronSource.Instance.onInterstitialAdDisplayedHandler -= OnInterDisplayed;
            AdsIronSource.Instance.onBannerHeightChanged -= OnBannerHeightChanged;
            AdsIronSource.Instance.onAdPaidRevenue -= OnAdPaidRevenue;
            AdsIronSource.Instance.onRewardFailedToLoad -= OnRewardAdLoadFailed;
            AdsIronSource.Instance.onInterFailedToLoad -= OnInterAdLoadFailed;
            AdsIronSource.Instance.onRewardFailedToShow -= OnRewardAdShowFailed;
            AdsIronSource.Instance.onInterFailedToShow -= OnInterAdShowFailed;
        }

#region Admob
        private void InitAdmob(AdSettings settings) {
            if (!settings.UseAOAAd && !settings.UseNativeAd) return;

            Debug.Log($"[Ads] Init admob useTest={settings.UseTestAd}");

            MobileAds.RaiseAdEventsOnUnityMainThread = true;
            MobileAds.Initialize(status => {
                Excutor.Schedule(() => {
                    if (settings.UseAOAAd && !Api.GDPR.IsFirstInstall) {
                        AdmobAppOpenAd.Instance.Init(settings.AdmobAOATierIds, settings.UseTestAd, () => {
                            if (!IsStillLoadingScene) Debug.Log($"AOA loaded but can not show because game scene is entered.");
                        }, null);
                    }

                    if (settings.UseNativeAd) {
                        if (!string.IsNullOrEmpty(settings.AdmobNativeId)) {
                            AdmobNativeAd.Instance.Init(settings.AdmobNativeId, settings.UseTestAd);      
                        }
                        else {
                            string remoteIdKey = string.Empty;
#if UNITY_ANDROID
                            remoteIdKey = "admob_native_id_android";
#else
                            remoteIdKey = "admob_native_id_ios";
#endif
                            RemoteConfig.Instance.GetStringAsync(remoteIdKey, (id) => { 
                                if (!string.IsNullOrEmpty(id)) AdmobNativeAd.Instance.Init(id, settings.UseTestAd);                        
                            });
                        }
                    }
                });
            });

            if (settings.UseAOAAd) {
                RemoteConfig.Instance.GetLongAsync("ad_aoa_capping", (value) => { settings.AoaResumeCapping = value; });
                RemoteConfig.Instance.GetBoolAsync("ad_aoa_resume", (value) => settings.AoaResumeEnable = value);
            }
        }

        private bool IsStillLoadingScene => SceneManager.GetActiveScene().buildIndex < AdSettings.Ins.AoaShowBeforeSceneIdx;

        
        public bool HasAOA => AdmobAppOpenAd.Instance.HasAOA;
        public void ShowAOA(Action callback = null) {
            if (IsRemovedAds || IsAdShowing || !HasAOA || IAP.IsPurchasing) {
                if (callback != null) callback.Invoke();
                return;
            }
            OnShowAdStart();
            AdmobAppOpenAd.Instance.ShowAOA(() => {
                OnShowAdFinished();
                if (callback != null) callback.Invoke();
            });
        }

#endregion

#region Callback Handler
        private void OnBannerHeightChanged() {
            if (IsRemovedAds) SetRemovedAds();
            onBannerHeightChanged?.Invoke();
        }

        private void OnRewardAdLoadFailed(string err) {
            Tracking.Instance.LogAdRewardLoadFailed(err);
        }
        private void OnRewardAdShowFailed(string err) {
            Tracking.Instance.LogAdRewardShowFailed(err);
        }

        private void OnInterAdLoadFailed(string err) {
            Tracking.Instance.LogAdInterLoadFailed(err);
        }
        private void OnInterAdShowFailed(string err) {
            Tracking.Instance.LogAdInterShowFailed(err);
        }

        private void OnRewardVideoAvailable() {
            onRewardVideoAvailable?.Invoke();
            Tracking.Instance.LogAdRewardAvailable();
        }

        private void OnRewardVideoDisplayed(string placement) {
            Tracking.Instance.LogAdRewardDisplayed(placement);
        }

        private void OnRewardVideoCompleted(string placement) {
            Tracking.Instance.LogAdRewardCompleted(placement);
        }

        private void OnRewardEligible() {
            Tracking.Instance.LogAdRewardEligible();
        }

        private void OnInterAvailable() {
            Tracking.Instance.LogAdInterAvailable();
        }
        
        private void OnInterEligible() {
            Tracking.Instance.LogAdInterEligible();
        }

        private void OnInterDisplayed(string placement) {
            Tracking.Instance.LogAdInterDisplayed(placement);

            InterShowCount++;
            Tracking.Instance.LogAdInterShowCount(placement, InterShowCount);
        }

        private void OnAdPaidRevenue(IronSourceAdInfo adInfo) {
            Tracking.Instance.LogAdImpression(Mediation.ironSource, adInfo.adNetwork, adInfo.instanceName, adInfo.adUnit, "USD", adInfo.revenue != null ? (double)adInfo.revenue : 0);
        }
#endregion Callback Handler
        
        public bool IsAdShowing { get; private set; }

        public bool HasInterstitial => !IsRemovedAds && AdsIronSource.Instance.HasInterstitial;

        public bool HasRewardVideo => AdsIronSource.Instance.HasRewardVideo;

        public void ShowBanner() {
            if (IsRemovedAds) return;
            AdsIronSource.Instance.ShowBanner();
        }

        public void HideBanner() {
            if (IsRemovedAds) return;
            AdsIronSource.Instance.HideBanner();
        }

        public void DestroyBanner() {
            if (!AdsIronSource.Initialized) return;
            AdsIronSource.Instance.DestroyBanner();
        }

        public float GetBannerHeight() {
            if (IsRemovedAds) return 0;
            return AdsIronSource.Instance.BannerHeight;
        }

        private bool InterCappingReady => Time.time - lastInterShowAtTime > AdSettings.Ins.InterCapping;        
        private int InterShowCount {
            get => PlayerPrefs.GetInt("AdInterShowCount", 0);
            set => PlayerPrefs.SetInt("AdInterShowCount", value);
        }

        /// <summary> Call this only in case AdSettings.AutoRequestInter = false </summary>
        public void RequestInterstitial() {
            if (AdSettings.Ins.AutoRequestInter) return;

            if (!AdsIronSource.Initialized || !AdsIronSource.Instance.InitCompleted) {
                AdSettings.Ins.AutoRequestInter = true;
                return;
            }

            AdsIronSource.Instance.RequestInterstitial();
        }

        public void ShowInterstitial(string placement, Action callback = null, bool capping = true) {
            OnInterEligible();
            if (IsRemovedAds || IAP.IsPurchasing || (capping && !InterCappingReady)) {
                callback?.Invoke();
                return;
            }

            OnShowAdStart();
            callback += OnShowAdFinished;

            if (AdsIronSource.Instance.HasInterstitial) {
                callback += () => { lastInterShowAtTime = Time.time; };
                AdsIronSource.Instance.ShowInterstitial(placement, callback);
            }
            else callback?.Invoke();
        }

        public void ShowRewardVideo(string placement, Action onSuccess, Action onClosed = null, bool showMsgAdNotReady = true) {
            OnRewardEligible();
            if (Logs.IsEditor || (Logs.CheatEnable && IsRemovedAds)) {
                onSuccess.Invoke();
                onClosed?.Invoke();
                return;
            }

            OnShowAdStart();
            onClosed += OnShowAdFinished;

            if (AdsIronSource.Instance.HasRewardVideo) {
                onSuccess += OnShowAdFinished;
                onSuccess += () => { OnRewardVideoCompleted(placement); };

                AdsIronSource.Instance.ShowRewardVideo(placement, onSuccess, onClosed);
            }
            else {
                if (showMsgAdNotReady) NoticeText.Instance.ShowNotice("AD NOT AVAILABLE!");
                onClosed?.Invoke();
            }
        }

        private void OnShowAdStart() {
            IsAdShowing = true;
            Logs.Log($"[AdsManager]: OnShowAdStart");
        }

        private void OnShowAdFinished() {
            if (!IsAdShowing) return;
            IsAdShowing = false;
            Logs.Log($"[AdsManager]: OnShowAdFinished");
        }

        private void OnApplicationPause(bool pause) {
            AdsIronSource.Instance.OnPause(pause);
            if (pause || IsAdShowing ) return; //|| PrefData.FirstPlayGame

            if (AdSettings.Ins.InterResumeEnable && HasInterstitial) {// && InterCappingReady) {
                ShowInterstitial("AppResume", null, false);
            }
            else if (AdSettings.Ins.AoaResumeEnable && Time.time > AdSettings.Ins.AoaResumeCapping) {
                ShowAOA();
            }

        }

    }
}