#if APPSFLYER

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AppsFlyerSDK;

namespace IronPirate.Api.Analytics {
    public class IPAppsFlyerAnalytic : SingletonBehaviourDontDestroy<IPAppsFlyerAnalytic> {
 
#if UNITY_EDITOR
        [UnityEditor.MenuItem("IRONPIRATE/Api/Analytics/AppsFlyerSettings")]
        public static void SelectSettings() {
            UnityEditor.Selection.activeObject = Resources.Load<AppsFlyerSettings>("AppsFlyer/AppsFlyerSettings");
        }
#endif

        protected override void OnAwake() {
            Init();
        }

        private void Init() {
            var settings = Resources.Load<AppsFlyerSettings>("AppsFlyer/AppsFlyerSettings");
            if (settings == null) {
                Logs.LogError("[APPSFLYER] Cannot load setting from resources path: AppsFlyer/AppsFlyerSettings");
                return;
            }

            AppsFlyer.setIsDebug(Logs.CheatEnable);

            AppsFlyer.initSDK(settings.DevKey, settings.AppID);
            AppsFlyer.startSDK();

            AppsFlyerAdRevenue.start();
            AppsFlyerAdRevenue.setIsDebug(Logs.CheatEnable);

            Logs.Log($"<color=green>[APPSFLYER] Initialized: SDK Version={AppsFlyer.getSdkVersion()}</color>");
        }

        public void LogEvent(string eventName) {
            this.Log(eventName, null);
        }

        public void LogEvent(string eventName, string paraName, string paraValue) {
            this.Log(eventName, new Dictionary<string, string>() { { paraName, paraValue } });
        }

        public void LogEvent(string eventName, ParameterBuilder parameterBuilder) {
            this.Log(eventName, parameterBuilder != null ? parameterBuilder.BuildDictString() : null);
        }

        public void Log(string eventName, Dictionary<string, string> para) {
            Logs.Log($"[APPSFLYER] eventName={eventName}, paraCount={(para != null ? para.Count : 0)}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            AppsFlyer.sendEvent(eventName, para);
        }

        public void LogAdRevenue(Mediation mediation, string network, double revenue, string currency, ParameterBuilder parameter) {
            var platform = mediation == Mediation.ironSource ? AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeIronSource
                : mediation == Mediation.appLovin ? AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeApplovinMax
                : AppsFlyerAdRevenueMediationNetworkType.AppsFlyerAdRevenueMediationNetworkTypeGoogleAdMob;
            Logs.Log($"[APPSFLYER] logAdRevenue, network={network}, revenue={revenue}{currency}");
            if (UnityEngine.Application.isEditor) {
                return;
            }
            AppsFlyerAdRevenue.logAdRevenue(network, platform, revenue, currency, parameter != null ? parameter.BuildDictString() : null);
        }

        public void LogPurchase(string productId, string currency, double revenue, int quantity = 1) {
            Dictionary<string, string> eventValues = new Dictionary<string, string>();
            eventValues.Add("af_productId", productId);
            eventValues.Add(AFInAppEvents.CURRENCY, !string.IsNullOrEmpty(currency) ? currency : "USD");
            eventValues.Add(AFInAppEvents.REVENUE, revenue.ToString());
            eventValues.Add("af_quantity", quantity.ToString());
            AppsFlyer.sendEvent(AFInAppEvents.PURCHASE, eventValues);
        }
    }
}
#endif