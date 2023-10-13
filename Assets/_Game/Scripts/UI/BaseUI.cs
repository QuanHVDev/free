using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseUIElement : MonoBehaviour {
	[SerializeField] Image bg;
	[SerializeField] Transform mainFrame;

	public abstract void OnAwake();
	public void Show(float toAlpha = 0.75f)
	{
		gameObject.SetActive(true);
		if (bg) {
			bg.DOKill();
			var c = bg.color;
			c.a = 0;
			bg.color = c;
			bg.DOFade(toAlpha, .3f).SetUpdate(true);
		}

		if (mainFrame) {
			mainFrame.DOKill();
			mainFrame.transform.localScale = Vector3.one * 0.5f;
			mainFrame.DOScale(1, 0.3f).SetEase(Ease.OutBack).SetUpdate(true);
		}
	}
	public virtual void Hide()
	{
		if (bg) {
			bg.DOKill();
			bg.DOFade(0, .3f).SetUpdate(true);
		}

		if (mainFrame) {
			mainFrame.DOKill();
			mainFrame.DOScale(0, 0.3f).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); });
		}
		else {
                
			gameObject.SetActive(false);
		}
	}
}