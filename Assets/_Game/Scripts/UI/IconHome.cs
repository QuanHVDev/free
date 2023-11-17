using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class IconHome : MonoBehaviour {
	public Image iconBG;
	
	public Transform homeModel { get; private set; }
	public Condition Condition { get; private set; }
	public void SetHomeModel(Transform homeModel) {
		gameObject.SetActive(true);
		this.homeModel = homeModel;
		Condition = null;
		if (homeModel.TryGetComponent(out Condition condition))
		{
			Condition = condition;
			StartCoroutine(WaitToAcceptCondition());
			DoFadeIcon(0, 0f);
		}
		else
		{
			DoFadeIcon(1, 0);
		}
	}

	public void DoFadeIcon(int value, float time) {
		iconBG.DOFade(value == 1 ? 155 / (255 * 1.0f) : value, time);
	}

	private IEnumerator WaitToAcceptCondition()
	{
		yield return new WaitUntil(() =>
		{
			return Condition.IsCanShow;
		});
		
		DoFadeIcon(1, 0);
	}
}
