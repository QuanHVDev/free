
using IronPirate.Api.Analytics;

namespace IronPirate {
    /**<summary> The main controller of game analytics. (Current use Facebook + Firebase + AppsFlyer) </summary>*/
    public partial class Tracking : SingletonBehaviourDontDestroy<Tracking> {
        public enum Service { All, Firebase, Facebook, AppsFlyer }

        public override void Preload() {
#if FACEBOOK
            IPFacebookAnalytic.Instance.Preload();
#endif

#if FIREBASE
            IPFirebaseAnalytic.Instance.Preload();
#endif

#if APPSFLYER
            IPAppsFlyerAnalytic.Instance.Preload();
#endif
        }

        public void LogEvent(string eventName, string eventParam, string paramValue, Service service = Service.All) {
            bool nullParam = string.IsNullOrEmpty(eventParam) || string.IsNullOrEmpty(paramValue);
#if FACEBOOK
            if (service == Service.All || service == Service.Facebook) {
                if (nullParam) IPFacebookAnalytic.Instance.LogEvent(eventName);
                else IPFacebookAnalytic.Instance.LogEvent(eventName, eventParam, paramValue);
            }
#endif

#if FIREBASE
            if (service == Service.All || service == Service.Firebase) {
                if (nullParam) IPFirebaseAnalytic.Instance.LogEvent(eventName);
                else IPFirebaseAnalytic.Instance.LogEvent(eventName, eventParam, paramValue);
            }
#endif

#if APPSFLYER
            if (service == Service.All || service == Service.AppsFlyer) {
                if (nullParam) IPAppsFlyerAnalytic.Instance.LogEvent(eventName);
                else IPAppsFlyerAnalytic.Instance.LogEvent(eventName, eventParam, paramValue);
            }
#endif
        }

        public void LogEvent(string eventName, ParameterBuilder builder = null, Service service = Service.All) {
#if FACEBOOK
            if (service == Service.All || service == Service.Facebook) {
                IPFacebookAnalytic.Instance.LogEvent(eventName, builder);
            }
#endif

#if FIREBASE
            if (service == Service.All || service == Service.Firebase) {
                IPFirebaseAnalytic.Instance.LogEvent(eventName, builder);
            }
#endif

#if APPSFLYER
            if (service == Service.All || service == Service.AppsFlyer) {
                IPAppsFlyerAnalytic.Instance.LogEvent(eventName, builder);
            }
#endif
        }

        public void LogAdImpression(Mediation mediation, string adNetwork, string adUnitName, string adType, string currency, double revenue) {
            if (mediation == Mediation.admob) revenue /= 1000000f;
            ParameterBuilder builder = ParameterBuilder.Create().Add("ad_platform", mediation.ToString())
                                                                .Add("ad_source", adNetwork)
                                                                .Add("ad_unit_name", adUnitName)
                                                                .Add("ad_format", adType)
                                                                .Add("currency", !string.IsNullOrEmpty(currency) ? currency : "USD")
                                                                .Add("value", revenue);

#if APPSFLYER
            IPAppsFlyerAnalytic.Instance.LogAdRevenue(mediation, adNetwork, revenue, currency, builder);
#endif

#if FIREBASE
            IPFirebaseAnalytic.Instance.LogAdImpression(builder);
#endif
        }

        public void LogPurchase(string packId, string currency, double revenue) {
#if APPSFLYER
            IPAppsFlyerAnalytic.Instance.LogPurchase(packId, currency, revenue * .65f);
#endif

#if FIREBASE
            IPFirebaseAnalytic.Instance.LogPurchase(packId, currency, revenue * .65f);
#endif
        }

        public void SetUserProperty(string propertyName, string propertyValue) {
#if FIREBASE
            IPFirebaseAnalytic.Instance.SetUserProperty(propertyName, propertyValue);
#endif
        }
    }

    public enum Mediation {
        admob,
        ironSource,
        appLovin, //Max
    }
}