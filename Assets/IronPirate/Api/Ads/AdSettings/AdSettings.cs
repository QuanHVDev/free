using System;
using UnityEngine;

namespace IronPirate.Api.Ads {   

    [CreateAssetMenu(fileName ="AdSettings", menuName ="IRONPIRATE/Api/AdSettings")]
    public class AdSettings : ScriptableObject {
        public bool UseTestAd = false;

        [Header("Admob - AOA Settings")]
        //Admob Test App Id Android: ca-app-pub-3940256099942544~3347511713
        //Admob Test App Id iOS: ca-app-pub-3940256099942544~1458002511
        public bool UseAOAAd = true;
        public bool AoaResumeEnable = true;
        public long AoaResumeCapping = 0;
        [Tooltip("AOA will be show when loading scene finished, before enter the game scene. If use GDPR set=2, else set=1")]
        public int AoaShowBeforeSceneIdx = 1;
        [SerializeField] string[] admobAOATierIds_Android;
        [SerializeField] string[] admobAOATierIds_iOS;

        [Header("Admob - Native Ad Settings")]
        public bool UseNativeAd = true;
        [SerializeField] string admobNativeId_Android;
        [SerializeField] string admobNativeId_iOS;

        [Header("Interstitial Ad Settings")]
        public bool UseInterAd = true;
        public bool InterResumeEnable = true;
        public long InterCapping = 0;
        public bool AutoRequestInter = true;

        [Header("Rewarded Ad Settings")]
        public bool UseRewardAd = true;        

        [Header("Banner Ad Settings")]
        public bool UseBannerAd = true;
        public bool AutoShowBanner = true;
        
        public string[] AdmobAOATierIds => IsAndroid ? admobAOATierIds_Android : admobAOATierIds_iOS;
        public string AdmobNativeId => IsAndroid ? admobNativeId_Android : admobNativeId_iOS;

        private bool IsAndroid {
            get {
#if UNITY_ANDROID
                return true;
#else
                return false;
#endif
            }
        }

#region Singleton
        private static AdSettings _ins;
        public static AdSettings Ins { 
            get {
                if (_ins != null) return _ins;
                _ins = Resources.Load<AdSettings>("AdSettings/AdSettings");
                if (_ins == null) {
                    Debug.LogError("[Ads] File not exist path='Resources/AdSettings/AdSettings' ");
                }

                return _ins;
            }
        }
#endregion

#if UNITY_EDITOR
        [UnityEditor.MenuItem("IRONPIRATE/Api/AdSettings")]
        public static void OpenAdSettingsFile() {
            UnityEditor.Selection.activeObject = UnityEditor.AssetDatabase.LoadAssetAtPath<AdSettings>("Assets/IronPirate/Api/Ads/AdSettings/Resources/AdSettings/AdSettings.asset");
        }

#endif

    }
}
