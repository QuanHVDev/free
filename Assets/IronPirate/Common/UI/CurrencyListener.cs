using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace IronPirate {
    public class CurrencyListener : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI currencyText;
        private ulong currencyCache;

        private void OnEnable() {
            GameEvents.onCashChanged += OnCurrencyChanged;
        }

        private void OnDisable() {
            GameEvents.onCashChanged -= OnCurrencyChanged;
        }

        private void Start() {
            currencyCache = PrefData.CurrentCash;
            OnCurrencyChanged(false);
        }

        private void OnCurrencyChanged(bool showCountingEff) {
            if (currencyCache >= PrefData.CurrentCash) {
                currencyCache = PrefData.CurrentCash;
                currencyText.text = PrefData.CurrentCash.ToString();
                return;
            }

            StopAllCoroutines();
            StartCoroutine(IECounting(currencyText, currencyCache, PrefData.CurrentCash, showCountingEff));
        }

        private IEnumerator IECounting(TextMeshProUGUI text, ulong from, ulong to, bool playCountingSfx) {
            if (from == to) yield break;

            if (playCountingSfx) SFX.Instance.PlayCurrencyCounting();

            float duration = 2;
            float elapse = 0;

            while (elapse < duration && currencyCache < to) {
                elapse += Time.deltaTime;
                currencyCache = (ulong)(Mathf.Lerp(from, to, elapse));
                text.text = currencyCache.ToString();
                yield return null;
            }

            currencyCache = to;
            text.text = to.ToString();
        }
    }
}