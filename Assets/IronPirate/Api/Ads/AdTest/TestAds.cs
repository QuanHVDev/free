using UnityEngine;

namespace IronPirate.Api.Ads.Test {
    public class TestAds : MonoBehaviour {

        private void Start() {
            AdsManager.Instance.Preload();
        }

        public void ShowInterstitial() {
            AdsManager.Instance.ShowInterstitial(null, () => {
                Debug.Log("[Test Ad] Interstitial completed.");
            });
        }

        
        public void ShowRewardVideo() {
            AdsManager.Instance.ShowRewardVideo(null, () => {
                Debug.Log("[Test Ad] RewardVideoAd earned reward.");
            }, () => {
                Debug.Log("[Test Ad] RewardVideoAd close.");
            });
        }
    }
}