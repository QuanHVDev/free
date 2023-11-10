using System.Collections;
using UnityEngine;

namespace IronPirate.ReloadRush {
    public class InternetChecking : SingletonBehaviourDontDestroy<InternetChecking> {
        [SerializeField] float loopSeconds = 10;
        [SerializeField] GameObject noInternetPopup;
        [SerializeField] Transform loadingIcon;

        protected override void OnAwake() {
            base.OnAwake();

            LoopChecking();
        }

        private void LoopChecking() {
            if (Application.internetReachability == NetworkReachability.NotReachable) {
                noInternetPopup.SetActive(true);
                Time.timeScale = 0;
                StartCoroutine(IEWaitForInternet());
            }
            else {
                Invoke(nameof(LoopChecking), loopSeconds);
            }
        }

        private IEnumerator IEWaitForInternet() {
            yield return new WaitUntil(() => Application.internetReachability != NetworkReachability.NotReachable);
            Time.timeScale = 1;
            noInternetPopup.SetActive(false);
            Invoke(nameof(LoopChecking), loopSeconds);
        }

        private void Update() {
            if (noInternetPopup.activeInHierarchy) {
                loadingIcon.Rotate(Vector3.forward, -.5f);
            }
        }
    }
}