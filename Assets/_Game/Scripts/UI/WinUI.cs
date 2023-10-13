using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinUI : BaseUIElement {
	public Action OnHideLine;
	[SerializeField] private Button btnNextLevel;
	public override void OnAwake() {
	}
	
	private void Start() {
		btnNextLevel.onClick.AddListener(() => {
			OnHideLine?.Invoke();
			GameManager.Instance.SpawnLevel();
			Hide();
		});
	}
	
}
