
using UnityEngine;
using System.Collections.Generic;

namespace IronPirate.Api.Analytics {
    [CreateAssetMenu(fileName = "ApiSettings", menuName = "IRONPIRATE/Api/ApiSettings")]
    internal class ApiSettings : ScriptableObject {
        public bool useGDPR = true;

        [Space(10)]
        public bool useIAP;
        public bool useRemoteConfig;
        public bool useCloudMessaging;
        public bool useInAppReview;

        [Header("===== Analytic =====")]
        public bool useFirebase;
        public bool useFacebook;
        public bool useAppsFlyer;

        public const string GDPRDefine = "GDPR";
        public const string IapDefine = "IAP";
        public const string RemoteDefine = "REMOTE";
        public const string CloudMessagingDefine = "FCM";
        public const string InAppReviewDefine = "IAR";

        public const string FirebaseDefine = "FIREBASE";
        public const string FacebookDefine = "FACEBOOK";
        public const string AppsFlyerDefine = "APPSFLYER";

    }

#if UNITY_EDITOR

    [UnityEditor.CustomEditor(typeof(ApiSettings))]
    internal class ApiSettingsEditor : UnityEditor.Editor {

        [UnityEditor.MenuItem("IRONPIRATE/Api/ApiSettings")]
        public static void OpenSettingsFile() {
            UnityEditor.Selection.activeObject = UnityEditor.AssetDatabase.LoadAssetAtPath<ApiSettings>("Assets/IronPirate/Api/Editor/ApiSettings.asset");
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUILayout.Space(20);
            if (GUILayout.Button("Save")) {
                SaveSetting();
            }
        }

        private void SaveSetting() {
            var setting = target as ApiSettings;

            Editor.ScriptingDefineHelper.UpdateSymbols(
                new KeyValuePair<string, bool>(ApiSettings.GDPRDefine, setting.useGDPR),
                new KeyValuePair<string, bool>(ApiSettings.IapDefine, setting.useIAP),
                new KeyValuePair<string, bool>(ApiSettings.InAppReviewDefine, setting.useInAppReview),
                new KeyValuePair<string, bool>(ApiSettings.RemoteDefine, setting.useRemoteConfig),
                new KeyValuePair<string, bool>(ApiSettings.CloudMessagingDefine, setting.useCloudMessaging),
                new KeyValuePair<string, bool>(ApiSettings.FirebaseDefine, setting.useFirebase),
                new KeyValuePair<string, bool>(ApiSettings.FacebookDefine, setting.useFacebook),
                new KeyValuePair<string, bool>(ApiSettings.AppsFlyerDefine, setting.useAppsFlyer)
            );

            UnityEditor.EditorUtility.SetDirty(UnityEditor.Selection.activeObject);
            UnityEditor.AssetDatabase.SaveAssets();
        }

    }
#endif
}
