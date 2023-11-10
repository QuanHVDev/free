using UnityEngine;
using System.Collections;

namespace IronPirate.Api.Analytics {
    [CreateAssetMenu(fileName = "AppsFlyerSettings", menuName = "IRONPIRATE/Api/Analytics/AppsFlyerSettings")]
    public class AppsFlyerSettings : ScriptableObject {
        [SerializeField] string devKey;
        [SerializeField][Tooltip("iOS ID as number")] string appId_iOS;

        public string DevKey => devKey;

        public string AppID {
            get {
#if UNITY_ANDROID
                return string.Empty;
#else
                return appId_iOS;
#endif
            }
        }
    }
}