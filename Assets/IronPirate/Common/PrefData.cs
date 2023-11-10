

using System;
using UnityEngine;

namespace IronPirate {
    public partial class PrefData {
        public static ulong CurrentCash {
            get {
                if (ulong.TryParse(PlayerPrefs.GetString("CurrentCash", "0"), out ulong result)) {
                    return result;
                }
                return 0;
            }
        }

        public static void SetCash(ulong value, bool showCountingEff = true) {
            PlayerPrefs.SetString("CurrentCash", value.ToString());
            GameEvents.Trigger(GameEvents.onCashChanged, showCountingEff);
        }
    }

    public partial class GameEvents {
        public static Action<bool> onCashChanged;
    }
}
