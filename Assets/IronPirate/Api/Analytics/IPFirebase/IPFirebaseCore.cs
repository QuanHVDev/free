
#if FIREBASE
using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase;
using Firebase.Analytics;

namespace IronPirate.Api.IPFirebase {
    public class IPFirebaseCore : SingletonBehaviourDontDestroy<IPFirebaseCore> {
#region Base
        public bool Available { get; private set; }
        private event Action initOnAvailable;

        protected override void OnAwake() {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith((Task<DependencyStatus> task) => {
                var dependencyStatus = task.Result;

                if (dependencyStatus == DependencyStatus.Available) {
                    Excutor.Schedule(OnFirebaseAvailable);
                }
                else {
                    Debug.LogErrorFormat("[FIREBASE] Could not resolve all Firebase dependencies: {0}", dependencyStatus);
                }
            });

        }

        private void OnFirebaseAvailable() {
            if (Available) return;
            Available = true;
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Logs.Log("[FIREBASE] OnFirebaseAvailable");

            if (initOnAvailable != null) {
                try { initOnAvailable.Invoke(); }
                catch { throw; }
                initOnAvailable = null;
            }
        }
#endregion

        public void InitModule(Action module) {
            if (module == null) return;

            if (Available) Excutor.Schedule(module);
            else initOnAvailable += module;
        }
    }
}
#endif