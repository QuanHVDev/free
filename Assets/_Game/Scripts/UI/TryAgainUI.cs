using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TryAgainUI : BaseUIElement {
	public Action OnTryAgain;
	
	[SerializeField] private Button btnTryAgain;
	public override void OnAwake() {
		
	}

	private void Start() {
		btnTryAgain.onClick.AddListener(TryAgain);
	}

	public void TryAgain() {
		OnTryAgain?.Invoke();
		Hide();
		GameManager.Instance.ResumeCamera(() =>
		{
			GameManager.Instance.NextLevel();
		});
	}
}
