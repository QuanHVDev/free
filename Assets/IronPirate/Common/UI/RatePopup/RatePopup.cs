using UnityEngine;
using UnityEngine.UI;
using IronPirate.ReloadRush;

namespace IronPirate {
    public class RatePopup : MonoBehaviour {
        [SerializeField] Color unrateColor;
        [SerializeField] Color rateColor;
        [SerializeField] Button rateButton;
        [SerializeField] Image[] rateStars;
        private UICtrl uICtrl;

        private void Start()
        {
            if(uICtrl is null) uICtrl = transform.root.GetComponent<UICtrl>();
        }


        private static bool canShow = false;
        private static int showAtSesionTimes = 2;

        private int starSelectedIdx = 0;

        private bool Rated {
            get => PlayerPrefs.HasKey("UserRated");
            set => PlayerPrefs.SetInt("UserRated", 1);
        }

        /// <summary> Call this only one times if you want to fetch value from remote config </summary>
        public void RequestRemoteConfig() {
#if FIREBASE && REMOTE
            RemoteConfig.Instance.GetBoolAsync("rating_popup", (result) => canShow = result);
            RemoteConfig.Instance.GetLongAsync("rating_location", (result) => showAtSesionTimes = (int)result);
#endif
        }

        public bool ShowIfAvailable(int sessionTimes, bool forceShow = false) {
            if (!canShow)  return false;
            if (forceShow) {
                gameObject.SetActive(true);
                return true;
            }
            else if (!Rated && sessionTimes == showAtSesionTimes) {
                gameObject.SetActive(true);
                return true;
            }

            return false;
        }

        private void OnEnable() {
            foreach (var rate in rateStars) {
                rate.color = unrateColor;
                rate.raycastTarget = true;
            }

            //rateStars[rateStars.Length - 1].DOColor(rateColor, 1).SetLoops(-1, LoopType.Yoyo);

            rateButton.gameObject.SetActive(false);
        }

        public void OnClickCloseButton() {
            this.gameObject.SetActive(false);
            uICtrl.CheckDroneOfferUI();
        }

        public void OnClickRateButton() {
            if (starSelectedIdx < rateStars.Length - 1) {
                OnClickCloseButton();
                return;
            }

#if UNITY_ANDROID
#if IAR
            InAppReview.Instance.RequestNativeReview(() => Rated = true);
#else
            Rated = true;
            Application.OpenURL(string.Format("market://details?id={0}", Application.identifier));
#endif
#elif UNITY_IOS

#endif
            OnClickCloseButton();
        }

        public void OnClickStartButton(int index) {
            starSelectedIdx = index;

            for (int i = 0; i < rateStars.Length; i++) {
                rateStars[i].raycastTarget = false;
                //rateStars[i].DOKill();
                if (i <= index) {
                    rateStars[i].color = rateColor;
                }
                else {
                    rateStars[i].color = unrateColor;
                }
            }

            Invoke(nameof(OnClickRateButton), 1f);

            //rateButton.gameObject.SetActive(true);
        }
    }
}

