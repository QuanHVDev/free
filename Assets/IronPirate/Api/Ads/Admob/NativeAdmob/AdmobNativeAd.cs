using GoogleMobileAds.Api;
using System;
using UnityEngine;

namespace IronPirate.Api.Ads {
    public class AdmobNativeAd {
        private static AdmobNativeAd instance;
        public static AdmobNativeAd Instance {
            get {
                if (instance == null) {
                    instance = new AdmobNativeAd();
                }

                return instance;
            }
        }

        public static bool Initialized => instance != null;

#if UNITY_ANDROID
        private const string ID_TEST_ADS = "ca-app-pub-3940256099942544/2247696110";

#elif UNITY_IOS
        private const string ID_TEST_ADS = "ca-app-pub-3940256099942544/3986624511";
#endif
        string unitId;
        int requestCount;
        bool isRequesting;
        public NativeAd CacheNativeAd { get; private set; }

        public event Action onAdNativeLoaded;

        private void Log(string msg, bool forceLog = false) {
            if (!forceLog) Logs.Log(string.Format("[Ads.Admod.Native] {0}", msg));
            else Debug.Log(string.Format("[Ads.Admod.Native] {0}", msg));
        }

        private void LogError(string msg) {
            Debug.LogError(string.Format("[Ads.Admod.Native] {0}", msg));
        }

        public void Init(string unitId, bool useAdTest) {
            if (string.IsNullOrEmpty(unitId)) return;
            this.unitId = unitId;
            if (useAdTest) this.unitId = ID_TEST_ADS;

            Log($"Initializing.. useTest= {useAdTest}", true);
            LoadNativeAd();
        }

        private void LoadNativeAd() {
            if (isRequesting) return;
            AdLoader adLoader = new AdLoader.Builder(unitId) .ForNativeAd().Build();
            adLoader.OnAdFailedToLoad += OnAdFailedToLoad;
            adLoader.OnNativeAdLoaded += OnAdLoaded;
            adLoader.OnNativeAdClosed += OnAdClosed;

            adLoader.LoadAd(new AdRequest());
            isRequesting = true;

            Log($"Start Request count = {requestCount}");
        }

        private void OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs arg) {
            LogError($"OnAdFailedToLoad err={(arg.LoadAdError != null ? arg.LoadAdError.GetMessage() : string.Empty)}");
            Excutor.Schedule(() => {
                Tracking.Instance.LogEvent("ad_native_load_fail", "errormsg", (arg.LoadAdError != null ? arg.LoadAdError.GetMessage() : string.Empty), Tracking.Service.Firebase);
            });

            isRequesting = false;
            requestCount++;
            Excutor.Schedule(LoadNativeAd, Mathf.Pow(2, requestCount));
        }

        private void OnAdLoaded(object sender, NativeAdEventArgs arg) {
            Log("OnAdLoaded", true);
            requestCount = 0;
            isRequesting = false;
            this.CacheNativeAd = arg.nativeAd;
            this.CacheNativeAd.OnPaidEvent += OnAdPaid;
            Excutor.Schedule(onAdNativeLoaded);
        }

        private void OnAdPaid(object sender, AdValueEventArgs arg) {
            Log("OnAdPaid", true);
            Excutor.Schedule(() => {
                Tracking.Instance.LogAdImpression(Mediation.admob, "admob", "native", "NATIVE", arg.AdValue.CurrencyCode, arg.AdValue.Value);
            });
        }

        private void OnAdClosed(object sender, EventArgs arg) {
            Log("OnAdClosed", true);
            this.CacheNativeAd = null;
            LoadNativeAd();
        }
    }
}
