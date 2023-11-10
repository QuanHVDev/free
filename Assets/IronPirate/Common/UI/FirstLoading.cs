using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

namespace IronPirate {
    public class FirstLoading : MonoBehaviour {
        [SerializeField] private int nextSceneIdx = 1;
        [SerializeField] private Image image_loading;
        [SerializeField] private Image blackFade;

        [SerializeField] float timeout = 6f;

        AsyncOperation asyncLoad;

        private void Awake() {
            Application.targetFrameRate = 60;
            if (timeout > 0) Invoke(nameof(Timeout), timeout);
        }
        private void Timeout() {
            timeout = 0;
        }

        private void Start() {
            StartCoroutine(LoadYourAsyncScene());
        }


        IEnumerator LoadYourAsyncScene() {
            asyncLoad = SceneManager.LoadSceneAsync(nextSceneIdx);
            asyncLoad.allowSceneActivation = false;
            float lastAmount = image_loading.fillAmount;

            float v = 1f / (timeout);

            while (!asyncLoad.isDone) {
                while (asyncLoad.progress < 0.9f || (timeout > 0 && WaitingForAOA)) {
                    image_loading.fillAmount = Mathf.Clamp(image_loading.fillAmount + v * Time.deltaTime, 0, .9f);
                    yield return null;
                }

                Debug.Log($"Loading end: asyncProgress={asyncLoad.progress}, timeout={timeout}, waitingAOA={WaitingForAOA}");
                image_loading.fillAmount = 1;
                yield return null;

                if (AdsManager.Instance.HasAOA) {
                    blackFade.gameObject.SetActive(true);
                    blackFade.DOFade(1, .1f).SetUpdate(true).OnComplete(() => {
                        AdsManager.Instance.ShowAOA();
                    });
                    yield return new WaitForSeconds(.2f);
                }

                yield return null;
                Debug.Log($"First splash loading finished. Enter game scene. total loading time={Time.time}");
                asyncLoad.allowSceneActivation = true;
                yield break;
            }

            asyncLoad = null;
        }

        private bool WaitingForAOA => !AdsManager.Instance.HasAOA;
    }
}