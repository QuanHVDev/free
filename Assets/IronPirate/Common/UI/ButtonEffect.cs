using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace IronPirate {

    [RequireComponent(typeof(UnityEngine.UI.Button))]
    public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler {
        [SerializeField] private bool playSound = true;
        [SerializeField] private bool scaleEffect = true;
        [SerializeField] private bool isAttention = false;

        Button btn; 

        private bool CanTouch => btn != null && btn.interactable;

        void Start() {
            btn = GetComponent<Button>();
            if (isAttention) RunAttention();
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (!CanTouch) return;
            if (scaleEffect) {
                transform.DOKill();
                transform.DOScale(1.1f, 0.2f);
            }
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (!CanTouch) return;
            if (scaleEffect) {
                transform.DOKill();
                transform.DOScale(1, 0.1f).SetUpdate(true);
            }
        }

        public void OnPointerClick(PointerEventData eventData) {
            if (!CanTouch) return;
            if (playSound) SFX.Instance.PlaySoundButton();
        }


        private void RunAttention() {
            transform.localScale = Vector3.one;
            transform.DOScale(.9f, 0.5f).SetUpdate(true).OnComplete(() => {
                transform.DOScale(1, 0.5f).SetUpdate(true).OnComplete(() => {
                    RunAttention();
                });
            });
        }


    }
}