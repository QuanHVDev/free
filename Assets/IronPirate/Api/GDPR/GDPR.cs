using DG.Tweening;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

namespace IronPirate.Api {
    public class GDPR : MonoBehaviour {
        [SerializeField] private Button btnY;
        [SerializeField] private Button btnN;
        [SerializeField] private Transform popUp;
        [SerializeField] private GameObject nextPreload;

        public static event Action onConsentCallback;
        public static bool IsFirstInstall { get; private set; } = false;
        public static bool HasConsent {
            get {
#if !GDPR
                Consent = true;
#endif
                return PlayerPrefs.HasKey("GDPR");
            }
        }

        public static bool Consent {
            get => PlayerPrefs.GetInt("GDPR", 1) == 1;
            set => PlayerPrefs.SetInt("GDPR", value ? 1 : 0);
        }

        private void Awake() {
            popUp.gameObject.SetActive(false);

            Excutor.Instance.Preload();
            RemoteConfig.Instance.Preload();
            RemoteConfig.Instance.AddDefault("ad_use_test_id", Ads.AdSettings.Ins.UseTestAd);
            RemoteConfig.Instance.GetBoolAsync("ad_use_test_id", (value) => Ads.AdSettings.Ins.UseTestAd = value );

            if (HasConsent) {
                Invoke(nameof(NextPreload), .2f);
                return;
            }
            IsFirstInstall = true;
        }

        private void Start() {
            if (HasConsent) return;
            popUp.gameObject.SetActive(true);
            popUp.localScale = Vector3.zero;
            popUp.DOScale(new Vector3(1, 1, 1), .5f).SetEase(Ease.OutQuad).OnComplete(() => {
                btnY.onClick.AddListener(() => OnClickYes());
                if (btnN) btnN.onClick.AddListener(() => OnClickNo());
            });

        }

        private void OnClickYes() {
            OnClick(true);
        }

        private void OnClickNo() {
            OnClick(false);
        }

        private void OnClick(bool acceptConsent) {
            Consent = acceptConsent;
            if (onConsentCallback != null) onConsentCallback.Invoke();
            popUp.DOScale(Vector3.zero, .5f).SetEase(Ease.OutQuad).OnComplete(NextPreload);
        }

        private void NextPreload() {
            popUp.gameObject.SetActive(false);// gameObject.SetActive(false);
            if (nextPreload) nextPreload.SetActive(true);
            Invoke(nameof(LoadNextScene), .1f);
        }

        private void LoadNextScene() {
            SceneManager.LoadScene(1);
        }

    }
}