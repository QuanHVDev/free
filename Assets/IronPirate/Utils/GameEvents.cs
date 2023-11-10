using System;
using UnityEngine;

namespace IronPirate {
    public partial class GameEvents {
        public static void Trigger(Action eventAction) {
            //Debug.LogFormat("[GameEvents] Trigger: ", nameof(eventAction)); 
            eventAction?.Invoke();
        }

        public static void Trigger<T>(Action<T> eventAction, T param) {
            //Debug.LogFormat("[GameEvents] Trigger: ", nameof(eventAction)); 
            eventAction?.Invoke(param);
        }

        public static void Trigger<T, K>(Action<T, K> eventAction, T param1, K param2) {
            //Debug.LogFormat("[GameEvents] Trigger: ", nameof(eventAction)); 
            eventAction?.Invoke(param1, param2);
        }
    }
}