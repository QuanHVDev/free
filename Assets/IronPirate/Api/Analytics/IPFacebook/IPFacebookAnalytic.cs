#if FACEBOOK

using System.Collections.Generic;

using IronPirate.Api.Facebooks;
using Facebook.Unity;

namespace IronPirate.Api.Analytics {
    public class IPFacebookAnalytic : SingletonBehaviourDontDestroy<IPFacebookAnalytic> {
        private bool available;
        private event System.Action callOnAvailable;

#if UNITY_EDITOR
        [UnityEditor.MenuItem("IRONPIRATE/Api/Analytics/Facebook Settings")]
        public static void SelectSettings() {
            UnityEditor.Selection.activeObject = UnityEngine.Resources.Load<Facebook.Unity.Settings.FacebookSettings>("FacebookSettings");
        }
#endif

        protected override void OnAwake() {
            IPFacebookCore.Instance.InitModule(InitFacebookAnalytics);
        }

        private void InitFacebookAnalytics() {
            if (available) return;
            available = true;
            if (callOnAvailable != null) callOnAvailable.Invoke();
            callOnAvailable = null;
        }

        //public void LogPurchase(float price, string currencyIsoCode = null, ParameterBuilder parameterBuilder) {

        //}

        //public void LogPurchase(float price, string currencyIsoCode = null, Dictionary<string, object> param = null) {
        //    FB.LogPurchase(price, currencyIsoCode, param);
        //}

        //public void LogPurchase(decimal price, string currencyIsoCode = null, Dictionary<string, object> param = null) {
        //    FB.LogPurchase(price, currencyIsoCode, param);
        //}

        public void LogEvent(string eventName) {
            this.LogEvent(eventName);
        }

        public void LogEvent(string eventName, string paraName, object paraValue) {
            this.LogEvent(eventName, null, new Dictionary<string, object>() { { paraName, paraValue } });
        }

        public void LogEvent(string eventName, float value, string paraName, object paraValue) {
            this.LogEvent(eventName, value, new Dictionary<string, object>() { { paraName, paraValue } });
        }

        public void LogEvent(string eventName, ParameterBuilder parameterBuilder) {
            this.LogEvent(eventName, null, parameterBuilder != null ? parameterBuilder.BuildDictObject() : null);
        }

        public void LogEvent(string eventName, float value, ParameterBuilder parameterBuilder) {
            this.LogEvent(eventName, value, parameterBuilder != null ? parameterBuilder.BuildDictObject() : null);
        }

        public void LogEvent(string eventName, float? value = null, Dictionary<string, object> para = null) {
            if (available) {
                Logs.Log(string.Format("[FACEBOOK] [{0}] value={1}, paraCount={2}", eventName, value != null ? value.ToString() : "null", para != null ? para.Count : 0));
                if (UnityEngine.Application.isEditor) return;

                try { FB.LogAppEvent(eventName, value, para); }
                catch { throw; }
            }
            else {
                Logs.Log(string.Format("[FACEBOOK] Not available yet! Push to callback: [{0}] value={1}, paraCount={2}", eventName, value != null ? value.ToString() : "null", para != null ? para.Count : 0));
                callOnAvailable += () => {
                    FB.LogAppEvent(eventName, value, para);
                };
            }
        }

    }
}
#endif