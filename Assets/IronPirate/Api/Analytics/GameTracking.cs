using System;
using IronPirate.Api.Analytics;

namespace IronPirate {
    public partial class Tracking {
        #region Event for Ads
        
        /// <summary> Trigger when call show inter (by logic game, include ad not available and no-ad had bought) </summary>
        public void LogAdInterEligible() {
            LogEvent("af_inters_ad_eligible", service: Service.AppsFlyer);
        }

        /// <summary> Trigger when inter ad loaded callback </summary>
        public void LogAdInterAvailable() {
            LogEvent("af_inters_api_called", service: Service.AppsFlyer);
            LogEvent("ad_inter_loaded", service: Service.Firebase);
        }

        /// <summary> Trigger when inter ad load failed or show failed callback </summary>
        public void LogAdInterLoadFailed(string err) {
            LogEvent("af_inters_api_failed", "errormsg", err, Tracking.Service.AppsFlyer);
            LogEvent("ad_inter_load_fail", "errormsg", err, Tracking.Service.Firebase);
        }

        public void LogAdInterShowFailed(string err) {
            LogEvent("af_inters_display_failed", "errormsg", err, Tracking.Service.AppsFlyer);
            LogEvent("ad_inter_show_fail", "errormsg", err, Tracking.Service.Firebase);
        }

        /// <summary> Trigger when inter ad displayed callback </summary>
        public void LogAdInterDisplayed(string placement) {
            LogEvent("af_inters_displayed", "placement", placement, service: Service.AppsFlyer);
            LogEvent("ad_inter_show", "placement", placement, service: Service.Firebase);
        }

        /// <summary> Trigger and count for each times of inter shown  </summary>
        public void LogAdInterShowCount(string placement, int count) {
            if (count <= 20) {
                LogEvent($"af_inters_displayed_{count}_times", "placement", placement, service: Service.AppsFlyer);
            }
        }

        /// <summary> Trigger when call show reward (by logic game, include ad not available) </summary>
        public void LogAdRewardEligible() {
            LogEvent("af_rewarded_ad_eligible", service: Service.AppsFlyer);
            LogEvent("ads_reward_click", service: Service.Firebase);
        }

        /// <summary> Trigger when reward ad loaded callback </summary>
        public void LogAdRewardAvailable() {
            LogEvent("af_rewarded_api_called", service: Service.AppsFlyer);
            LogEvent("ad_reward_loaded", service: Service.Firebase);
        }

        public void LogAdRewardLoadFailed(string err) {
            LogEvent("af_rewarded_api_failed", "errormsg", err, Tracking.Service.AppsFlyer);
            LogEvent("ad_reward_load_fail", "errormsg", err, Tracking.Service.Firebase);
        }

        public void LogAdRewardShowFailed(string err) {
            LogEvent("af_rewarded_display_failed", "errormsg", err, Tracking.Service.AppsFlyer);
            LogEvent("ad_reward_show_fail", "errormsg", err, Tracking.Service.Firebase);
        }

        /// <summary> Trigger when reward ad displayed callback </summary>
        public void LogAdRewardDisplayed(string placement) {
            LogEvent("af_rewarded_displayed", "placement", placement, service: Service.AppsFlyer);
            LogEvent("ad_reward_show", "placement", placement, service: Service.Firebase);
        }

        public void LogAdRewardCompleted(string placement) {
            LogEvent("af_rewarded_completed", "placement", placement, service: Service.AppsFlyer);
            LogEvent("ads_reward_completed", "placement", placement, service: Service.Firebase);
        }
        #endregion

        public void LogLevelStart(int lv) {
            if (lv <= 20) {
                LogEvent($"af_level_start", "lv", lv.ToString(), service: Service.AppsFlyer);
            }
            LogEvent($"start_level_{lv}", service: Service.Firebase);
        }

        public void LogLevelWin(int lv) {
            if (lv <= 20) {
                LogEvent($"af_level_win", "lv", lv.ToString(), service: Service.AppsFlyer);
            }
            LogEvent($"win_level_{lv}", service: Service.Firebase);
        }

        public void LogLevelLose(int lv) {
            LogEvent($"af_level_lose", "lv", lv.ToString(), service: Service.AppsFlyer);
            LogEvent($"lose_level_{lv}", service: Service.Firebase);
        }

        public void LogLevelCompleted(int lv) {
            if (lv <= 20) {
                LogEvent($"completed_level_{lv}", "lv", lv.ToString(), service: Service.AppsFlyer);
            }
        }

        public void LogButtonClick(string buttonName, string placement = null) {
            LogEvent(buttonName, "placement", placement, service: Service.Firebase);
        }

        public void LogGamePlay(bool startGame) {
            LogEvent(startGame ? "start_game" : "clear_game");
        }
    }
}
