using System;
using UnityEngine;
using System.Collections.Generic;

namespace IronPirate.Api.Ads {
    /// <summary>
    /// <para>NOTICE: IronSource RewardVideo will be load automaticly when SDK initialized, can not call request it as same as banner or interstitial.</para>
    /// </summary>
    public class AdsIronSource : SingletonBehaviourDontDestroy<AdsIronSource> {
        public bool InitCompleted { get; private set; }
        public bool UseTestAd => AdSettings.Ins.UseTestAd;
        public bool UseInterstitialAd => AdSettings.Ins.UseInterAd;
        public bool UseRewardedVideoAd => AdSettings.Ins.UseRewardAd;

        public bool UseBannerAd => AdSettings.Ins.UseBannerAd;
        public bool AutoShowBanner => AdSettings.Ins.AutoShowBanner;
        public bool AutoRequestInter => AdSettings.Ins.AutoRequestInter;

        public bool ShowBannerOnBottom = true;
        bool isBannerShowing;
        string currentInterPlacement, currentRewardPlacement;

        /// <summary>
        /// Use this for listening when the game has reward video ad ready to show, usefull for turn on button ads
        /// </summary>
        public event Action onInterstitialAdLoadedHandler, onRewardVideoAdLoadedHandler;
        public event Action<string> onInterstitialAdDisplayedHandler, onRewardVideoAdDisplayedHandler;
        public event Action onBannerHeightChanged;

        public event Action<string> onRewardFailedToLoad, onInterFailedToLoad;
        public event Action<string> onRewardFailedToShow, onInterFailedToShow;
        public event Action<IronSourceAdInfo> onAdPaidRevenue;
        Action onRewardSuccess, onRewardClose;
        Action onInterCallback;

        bool isRequestingBanner = false, isRequestingInterstitial = false, isRequestingRewardVideo = false;//, getRewardSuccess;

        private readonly int[] retryTimes = { 0, 2, 5, 10, 20, 60, 60, 120, 120, 240, 240, 400, 400, 600 };
        protected int retryRewardVideo = 0, retryInterstial = 0, retryBanner = 0;

        protected override void  OnAwake() {
            var config = Resources.Load<IronSourceMediationSettings>(IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME);
            if (config == null) {
                Debug.LogError($"{IronSourceConstants.IRONSOURCE_MEDIATION_SETTING_NAME} file not found at Resources! Re-install SDK if needed!");
                return;
            }

#if UNITY_IOS
            string appID = UseTestAd ? "8545d445" : config.IOSAppKey;
#else
            string appID = UseTestAd ? "85460dcd" : config.AndroidAppKey;
#endif
            if (string.IsNullOrEmpty(appID)) {
                Debug.LogError("Need to config app key first!");
                return;
            }

            Debug.Log($"[Ads.IronSource] SDK Initializing {appID} pluginVersion={IronSource.pluginVersion()}, deviceID={IronSource.Agent.getAdvertiserId()}, UseTestAd={UseTestAd}, useBanner={UseBannerAd}, useInter={UseInterstitialAd}, useReward={UseRewardedVideoAd}");

            InitCompleted = false;
            IronSourceEvents.onSdkInitializationCompletedEvent += OnInitCompleted;

            IronSource.Agent.validateIntegration();

            IronSource.Agent.setAdaptersDebug(Logs.IsEnable);
            IronSource.Agent.setConsent(GDPR.Consent);

            IronSource.Agent.init(appID);

#if ADQUALITY
            var adQuality = new AdQualityIronSource();
            adQuality.Init(appID, Logs.IsEnable);
#endif
        }

        private void OnInitCompleted() {
            if (InitCompleted) return;
            InitCompleted = true;
            Log("", $"Initialize completed", true);
            InitBannerAd();
            InitInterstitialAd();
            InitRewardVideoAd();
        }

        public void OnPause(bool isPaused) {
            IronSource.Agent.onApplicationPause(isPaused);
        }

        private void Log(string adType, string msg, bool forceLog = false) {
            if (!forceLog) {
                Logs.Log($"[Ads.IronSource.{adType}] {msg}");
            }
            else Debug.Log($"[Ads.IronSource.{adType}] {msg}");
        }

        private void LogError(string adType, string msg) {
            Logs.LogError($"[Ads.IronSource.{adType}] {msg}");
        }

#region Banner
        private void OnBannerHeightChangedCallback() {
            if (onBannerHeightChanged != null) onBannerHeightChanged.Invoke();
        }

        private IronSourceBannerSize bannerSize;
        private void InitBannerAd() {
            IronSourceBannerEvents.onAdLoadedEvent += OnBannerAdLoaded;
            IronSourceBannerEvents.onAdLoadFailedEvent += OnBannerAdFailedToLoad;
            IronSourceBannerEvents.onAdScreenPresentedEvent += OnBannerAdDisplay;
            IronSourceBannerEvents.onAdScreenDismissedEvent += OnBannerClosed;
            
            bannerSize = IronSourceBannerSize.SMART;
            bannerSize.SetAdaptive(true);
            if (AutoShowBanner) RequestBanner();
        }

        public void RequestBanner() {
            if (!UseBannerAd || isRequestingBanner) return;

            if (retryBanner >= retryTimes.Length) retryBanner = retryTimes.Length - 1;
            int time = retryTimes[retryBanner];
            isRequestingBanner = true;
            Log("Banner", $"Will Request after {time}s, retry={retryBanner}");

            Excutor.Schedule(DoRequestBanner, time, true);
        }

        private void DoRequestBanner() {
            Log("Banner", "Request starting...", true);
            DestroyBanner();
            IronSource.Agent.loadBanner(IronSourceBannerSize.SMART, ShowBannerOnBottom ? IronSourceBannerPosition.BOTTOM : IronSourceBannerPosition.TOP);
        }

        public void ShowBanner() {
            if (isBannerShowing) return;

            if (!AutoShowBanner) RequestBanner();
            else {
                isBannerShowing = true;
                IronSource.Agent.displayBanner();
            }
        }

        public void HideBanner() {
            isBannerShowing = false;
            IronSource.Agent.hideBanner();
        }

        public void DestroyBanner() {
            isBannerShowing = false;
            IronSource.Agent.destroyBanner();
        }

        /// <summary>
        /// The height in pixel of the banner, use for controll your UI follow up banner.
        /// <para>Example: when user buy removed ads, the bottom button should be move down than normal.</para>
        /// </summary>
        public float BannerHeight => bannerSize.Height;

#region Banner callback handlers
        private void OnBannerAdLoaded(IronSourceAdInfo adInfo) {
            Log("Banner", "OnAdLoaded.", true);
            retryBanner = 0;
            isRequestingBanner = false;
            isBannerShowing = true;
            Excutor.Schedule(OnBannerHeightChangedCallback);
        }

        private void OnBannerAdFailedToLoad(IronSourceError err) {
            LogError("Banner", err != null ?  $"OnAdFailedToLoad: errorCode={err.getErrorCode()}: {err.getDescription()}" : "OnAdFailedToLoad");
            isRequestingBanner = false;
            isBannerShowing = false;
            retryBanner++;
            RequestBanner();
        }

        private void OnBannerAdDisplay(IronSourceAdInfo adInfo) {
            Log("Banner", "OnAdOpened...");
            Excutor.Schedule(onAdPaidRevenue, adInfo);            
            Excutor.Schedule(OnBannerHeightChangedCallback);
        }

        private void OnBannerAdClicked() {

        }

        private void OnBannerClosed(IronSourceAdInfo adInfo) {
            Log("Banner", "OnBannerClosed. Request a new one");
            isBannerShowing = false;
            if (AutoShowBanner) RequestBanner();
        }
#endregion Banner Callback
#endregion Banner Ad

#region Interstitial
        private void InitInterstitialAd() {
            IronSourceInterstitialEvents.onAdReadyEvent += OnInterstitialAdLoaded;
            IronSourceInterstitialEvents.onAdLoadFailedEvent += OnInterstitialAdFailedToLoad;
            IronSourceInterstitialEvents.onAdShowFailedEvent += OnInterstitialAdFailedToShow;
            IronSourceInterstitialEvents.onAdShowSucceededEvent += OnInterstitialAdOpened;
            IronSourceInterstitialEvents.onAdClosedEvent += OnInterstitialAdClosed;

            if (AutoRequestInter) RequestInterstitial();
        }

        public void RequestInterstitial() {
            if (!UseInterstitialAd || isRequestingInterstitial || IronSource.Agent.isInterstitialReady()) return;

            if (retryInterstial >= retryTimes.Length) retryInterstial = retryTimes.Length - 1;
            int time = retryTimes[retryInterstial];
            isRequestingInterstitial = true;
            Log("Interstitial", $"Request after {time}s, retry={retryInterstial}");

            Excutor.Schedule(DoRequestInterstitial, time, true);
        }

        private void DoRequestInterstitial() {
            Log("Interstitial", "Request starting...", true);
            IronSource.Agent.loadInterstitial();
        }

        public bool HasInterstitial {
            get {
                bool loaded = IronSource.Agent.isInterstitialReady();
                if (loaded) return true;
                RequestInterstitial();
                return false;
            }
        }

        public void ShowInterstitial(string placement, Action onSuccess = null) {
            if (HasInterstitial) {
                onInterCallback = onSuccess;
                currentInterPlacement = placement;
                IronSource.Agent.showInterstitial(placement);
                CancelTimeout();
                Invoke(nameof(OnInterTimeout), 20);
            }
            else {
                onSuccess?.Invoke();
            }
        }
        
        private void OnInterTimeout() {
            if (onInterCallback == null && currentInterPlacement == null) return;
            Log("Interstitial", $"OnAdTimeout placement={currentInterPlacement}", true);
            onInterCallback.Invoke();
            onInterCallback = null;
            currentInterPlacement = null;
            RequestInterstitial();
        }

        private void CancelTimeout() {
            if (IsInvoking(nameof(OnInterTimeout))) CancelInvoke(nameof(OnInterTimeout));
        }

        public void DestroyInterstitial() {
        }

#region Interstitial callback handlers
        public void OnInterstitialAdLoaded(IronSourceAdInfo adInfo) {
            Log("Interstitial", adInfo != null ? $"OnAdLoaded network={adInfo.adNetwork} by onAdReadyEvent" : "OnAdLoaded by onAdReadyEvent.", true);
            retryInterstial = 0;
            if (isRequestingInterstitial) Excutor.Schedule(onInterstitialAdLoadedHandler);
            isRequestingInterstitial = false;
        }

        public void OnInterstitialAdFailedToLoad(IronSourceError err) {
            LogError("Interstitial", $"OnAdFailedToLoad: errorCode={err.getErrorCode()}: {err.getDescription()}");
            isRequestingInterstitial = false;

            retryInterstial++;
            RequestInterstitial();
            Excutor.Schedule(onInterFailedToLoad, string.Format("{0}_{1}", err.getErrorCode(), err.getDescription()));
        }

        private void OnInterstitialAdFailedToShow(IronSourceError err, IronSourceAdInfo adInfo) {
            LogError("Interstitial", err != null ? $"OnAdFailedToShow: placement={currentInterPlacement} error={err.getErrorCode()} : {err.getDescription()}" : $"OnAdFailedToShow placement={currentInterPlacement}");
            Excutor.Schedule(onInterCallback);
            onInterCallback = null;
            currentInterPlacement = null;
            CancelTimeout();
            RequestInterstitial();
            Excutor.Schedule(onInterFailedToShow, string.Format("{0}_{1}", err.getErrorCode(), err.getDescription()));
        }

        public void OnInterstitialAdOpened(IronSourceAdInfo adInfo) {
            Log("Interstitial", adInfo != null ? $"OnAdOpened network={adInfo.adNetwork} placement={currentInterPlacement}" : $"OnAdOpened placement={currentInterPlacement}", true);
            Excutor.Schedule(onInterstitialAdDisplayedHandler, currentInterPlacement);
            Excutor.Schedule(onAdPaidRevenue, adInfo);
        }

        public void OnInterstitialAdClosed(IronSourceAdInfo adInfo) {
            Log("Interstitial", adInfo != null ? $"OnAdClosed network={adInfo.adNetwork} placement={currentInterPlacement}" : $"OnAdClosed placement={currentInterPlacement}", true);
            Excutor.Schedule(onInterCallback);
            onInterCallback = null;
            currentInterPlacement = null;
            CancelTimeout();
            RequestInterstitial();
        }

        public void onInterstitialClicked(string msg) {

        }

        public void onInterstitialExpired() {
            Log("Interstitial", "OnAdExpired. Request new one.");
            RequestInterstitial();
        }
#endregion Interstitial callback
#endregion

#region Reward video
        private void InitRewardVideoAd() {
            IronSourceRewardedVideoEvents.onAdAvailableEvent += OnRewardedVideoAdLoaded;
            IronSourceRewardedVideoEvents.onAdLoadFailedEvent += OnRewardedVideoAdFailedToLoad;
            IronSourceRewardedVideoEvents.onAdUnavailableEvent += OnRewardedVideoUnavailable;
            IronSourceRewardedVideoEvents.onAdOpenedEvent += OnRewardedVideoAdOpened;
            IronSourceRewardedVideoEvents.onAdClosedEvent += OnRewardedVideoAdClosed;
            IronSourceRewardedVideoEvents.onAdRewardedEvent += OnRewardedVideoEarnedReward;
            IronSourceRewardedVideoEvents.onAdShowFailedEvent += OnRewardedVideoAdFailedToShow;

            RequestRewardVideo();
        }

        private void RequestRewardVideo() {
            return;// Do nothing because SDK is auto request for rewarded video

            if (!UseRewardedVideoAd || isRequestingRewardVideo) return;

            if (retryRewardVideo >= retryTimes.Length) retryRewardVideo = retryTimes.Length - 1;
            int time = retryTimes[retryRewardVideo];
            isRequestingRewardVideo = true;
            Log("RewardedVideo", $"Request after {time}s, retry={retryRewardVideo}");

            Excutor.Schedule(DoRequestRewardVideo, time, true);
        }

        private void DoRequestRewardVideo() {
            Log("RewardedVideo", "Request starting...");
            IronSource.Agent.loadRewardedVideo();
        }

        public bool HasRewardVideo {
            get {
                bool loaded = IronSource.Agent.isRewardedVideoAvailable();
                if (loaded) return true;
                RequestRewardVideo();
                return false;
            }
        }

        public void ShowRewardVideo(string placement, Action onSuccess, Action onClosed = null) {
            if (HasRewardVideo) {
                onRewardSuccess = onSuccess;
                onRewardClose = onClosed;
                currentRewardPlacement = placement;
                IronSource.Agent.showRewardedVideo(placement);
            }
            else {
                onClosed?.Invoke();
            }
        }

#region RewardVideo callback handlers
        private void OnRewardedVideoAdLoaded(IronSourceAdInfo adInfo) {
            Log("RewardedVideo", adInfo != null ? $"OnAdLoaded network={adInfo.adNetwork}" :  "OnAdLoaded.", true);
            retryRewardVideo = 0;
            isRequestingRewardVideo = false;

            Excutor.Schedule(onRewardVideoAdLoadedHandler);
        }

        private void OnRewardedVideoUnavailable() {
            LogError("RewardedVideo", "OnAdUnavailable");
        }

        public void OnRewardedVideoAdFailedToLoad(IronSourceError err) {
            LogError("RewardedVideo", err != null ? $"OnAdFailedToLoad: err={err.getErrorCode()}: {err.getDescription()}" : "OnAdFailedToLoad");
            isRequestingRewardVideo = false;
            retryRewardVideo++;
            RequestRewardVideo();
            Excutor.Schedule(onRewardFailedToLoad, string.Format("{0}_{1}", err.getErrorCode(), err.getDescription()));
        }

        private void OnRewardedVideoAdFailedToShow(IronSourceError err, IronSourceAdInfo adInfo) {
            LogError("RewardedVideo", err != null ? $"OnAdFailedToShow: errorCode={err.getErrorCode()}: {err.getDescription()}" : "OnAdFailedToShow");
            Excutor.Schedule(onRewardClose);
            onRewardClose = null;
            onRewardSuccess = null;
            RequestRewardVideo();
            Excutor.Schedule(onRewardFailedToShow, string.Format("{0}_{1}", err.getErrorCode(), err.getDescription()));
        }

        private void OnRewardedVideoAdOpened(IronSourceAdInfo adInfo) {
            Log("RewardedVideo", adInfo != null ? $"OnAdOpening network={adInfo.adNetwork}" : "OnAdOpening...", true);
            Excutor.Schedule(onRewardVideoAdDisplayedHandler, currentRewardPlacement);
            Excutor.Schedule(onAdPaidRevenue, adInfo);
        }

        public void OnRewardedVideoEarnedReward(IronSourcePlacement placement, IronSourceAdInfo adInfo) {
            Log("RewardedVideo", "OnAdEarnedReward successfully.", true);
            Excutor.Schedule(onRewardSuccess);
        }

        public void OnRewardedVideoAdClosed(IronSourceAdInfo adInfo) {
            Log("RewardedVideo", adInfo != null ? $"OnAdClosed network={adInfo.adNetwork}" : "OnAdClosed.", true);
            Excutor.Schedule(onRewardClose);
            onRewardClose = null;
            currentRewardPlacement = null;
            RequestRewardVideo();
        }

        public void OnRewardedVideoExpired() {
            Log("RewardedVideo", $"OnAdExpired. Request a new one.");
        }

        public void OnRewardedVideoClicked() {

        }
#endregion RewardVideo callback
#endregion

    }
}