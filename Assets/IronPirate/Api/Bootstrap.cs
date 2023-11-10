using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

namespace IronPirate.Api {
    /// <summary>
    /// Place this into the first scene
    /// </summary>
    public partial class Bootstrap : MonoBehaviour {
        [SerializeField] private bool preloadRemoteConfig = true;
        [SerializeField] private bool preloadAnalytics = true;
        [SerializeField] private bool preloadCloudMessaging = true;
        [SerializeField] private bool preloadAds = true;
        [SerializeField] private bool preloadIAP = true;

        void Awake() {
            Application.targetFrameRate = 60;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            Excutor.Instance.Preload();
            if (GDPR.HasConsent) Init();
            else {
                GDPR.onConsentCallback += Init;
            }
        }

        private void Init() {
#if UNITY_IOS
            // check with iOS to see if the user has accepted or declined tracking
            var status = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();

            if (status == ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED) {
                StartCoroutine(IEWaitForRequestATT(PreloadApi));
            }

#else
            PreloadApi();
#endif

        }

#if UNITY_IOS
        private IEnumerator IEWaitForRequestATT(System.Action callback) {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            yield return new WaitUntil(() => ATTrackingStatusBinding.GetAuthorizationTrackingStatus() != ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED);

            if (callback != null) callback.Invoke();
        }
#endif

        private void PreloadApi() {
            if (preloadRemoteConfig) {
                PreloadRemoteConfig();
            }

            if (preloadCloudMessaging) {
                PreloadCloudMessaging();
            }

            if (preloadAnalytics) {
                PreloadAnalytics();
            }

            if (preloadIAP) {
                PreloadIAP();
            }

            if (preloadAds) {
                PreloadAds();
            }

        }

        partial void PreloadAnalytics();
        partial void PreloadCloudMessaging();
        partial void PreloadAds();
        partial void PreloadIAP();
        partial void PreloadRemoteConfig();

    }
}