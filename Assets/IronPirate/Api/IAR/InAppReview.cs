using System;
using System.Collections;

#if IAR && UNITY_ANDROID
using Google.Play.Review;
using UnityEngine;
#endif

namespace IronPirate {
    public class InAppReview : SingletonBehaviour<InAppReview> {

        private bool isRequesting;

        protected override void OnAwake() {
            base.OnAwake();
        }

        public void RequestNativeReview(Action callback = null) {
            if (isRequesting) return;
#if IAR && UNITY_ANDROID
            isRequesting = true;
            StartCoroutine(IERequestNativeReview(callback));
#endif
        }

#if IAR && UNITY_ANDROID
        public IEnumerator IERequestNativeReview(Action callback = null) {
            var reviewMng = new ReviewManager();

            Debug.Log("[IAR] Start request native review..");
            var requestFlowOperation = reviewMng.RequestReviewFlow();
            yield return requestFlowOperation;

            if (requestFlowOperation.Error != ReviewErrorCode.NoError) {
                Logs.LogError($"[IAR] {requestFlowOperation.Error.ToString()}");
                isRequesting = false;
                yield break;
            }

            Debug.Log("[IAR] Start lauch review flow..");
            var launchFlowOperation = reviewMng.LaunchReviewFlow(requestFlowOperation.GetResult());
            yield return launchFlowOperation;

            if (launchFlowOperation.Error != ReviewErrorCode.NoError) {
                // Log error. For example, using requestFlowOperation.Error.ToString().
                Logs.LogError($"[IAR] {launchFlowOperation.Error.ToString()}");
                yield break;
            }

            Debug.Log("[IAR] Review finished");

            isRequesting = false;
            if (callback != null) callback.Invoke();
        }
#endif
    }
}