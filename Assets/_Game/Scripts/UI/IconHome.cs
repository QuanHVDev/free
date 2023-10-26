using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class IconHome : MonoBehaviour {
	public Image iconBG;
	
	public Transform homeModel { get; private set; }

	public void SetHomeModel(Transform homeModel) {
		this.homeModel = homeModel;
		DoFadeIcon(1, 0);
	}

	public void DoFadeIcon(int value, float time) {
		iconBG.DOFade(value == 1 ? 155 / (255 * 1.0f) : value, time);
	}
}
