
#if FIREBASE && FCM
using IronPirate.Api.IPFirebase;
using System;
#endif

using UnityEngine;

namespace IronPirate {

    public partial class PrefData {
        public static string FCMToken {
            get => PlayerPrefs.GetString("FCMToken");
            set => PlayerPrefs.SetString("FCMToken", value);
        }
    }

    public class CloudMessaging : SingletonBehaviourDontDestroy<CloudMessaging> {
        protected override void OnAwake() {
#if FIREBASE && FCM
            IPFirebaseCore.Instance.InitModule(InitFirebaseMessaging);
#else
            Debug.Log("Flag FIREBASE & FCM was not set for Firebase Cloud Messaging");
#endif
        }

        private void InitFirebaseMessaging() {
#if FIREBASE && FCM
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;
#endif
        }

#if FIREBASE && FCM
        private void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token) {
            Debug.Log("[FIREBASE] Received Registration Token: " + token.Token);
            PrefData.FCMToken = token.Token;
        }

        private void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e) {
            var notification = e.Message.Notification;
            string title = string.Empty;
            string body = string.Empty;
            string chanelID = "Default";

            if (notification != null) {
                title = notification.Title;
                body = notification.Body;
                if (notification.Android != null) {
                    chanelID = notification.Android.ChannelId;
                }
            }

            Logs.Log($"[FIREBASE] Received a new message from: {e.Message.From}, title={title}, body={body}");

            Assets.SimpleAndroidNotifications.NotificationManager.Send(TimeSpan.Zero, title, body, Color.white);
        }
#endif
    }
}