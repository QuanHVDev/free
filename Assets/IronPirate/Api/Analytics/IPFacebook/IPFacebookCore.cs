#if FACEBOOK
using Facebook.Unity;
#endif
using System.Diagnostics;

namespace IronPirate.Api.Facebooks {
    public class IPFacebookCore : SingletonBehaviourDontDestroy<IPFacebookCore> {
        private bool initing;
        private bool available;
        private event System.Action callOnAvailable;

        public static bool IsEnable {
#if FACEBOOK
            get => true;
#else
            get => false;
#endif
        }

        protected override void OnAwake() {
            InitOrResume();
        }

        void OnApplicationPause(bool pauseStatus) {
            if (!pauseStatus) {
                InitOrResume();
            }
        }

        private void InitOrResume() {
#if FACEBOOK
            if (FB.IsInitialized) {
                FB.ActivateApp();
            }
            else if (!initing) {
                initing = true;
                FB.Init(() => {
                    Logs.Log("[FACEBOOK] Initialized");
                    FB.ActivateApp();
                    available = true;
                    if (callOnAvailable != null) callOnAvailable.Invoke();
                    callOnAvailable = null;
                });
            }
#endif
        }

        public void InitModule(System.Action action) {
#if FACEBOOK
            if (action == null) return;
            if (available) action.Invoke();
            else callOnAvailable += action;
#else
            Logs.LogError("[FACEBOOK] ScriptingDefine is not set: FACEBOOK (PlayerSetting/Scripting Define");
#endif
        }
    }
}