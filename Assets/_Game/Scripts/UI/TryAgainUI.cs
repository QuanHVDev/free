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
		btnTryAgain.onClick.AddListener(() => {
			OnTryAgain?.Invoke();
			Hide();
			GameManager.Instance.SpawnLevel();
		});
	}
}
