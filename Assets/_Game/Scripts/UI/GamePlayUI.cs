using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GamePlayUI : MonoBehaviour
{
	[SerializeField] private IconHomeManagerUI iconHomeManagerUI;
	[SerializeField] private IconPeopleManagerUI iconPeopleManagerUI;
	[SerializeField] private IconHealthManagerUI iconHealthManagerUI;
	[SerializeField] private TryAgainUI tryAgainUI;
	[SerializeField] private MessageManagerUI messageManagerUI;
	[SerializeField] private WinUI winUI;

	private void Start() {
		iconHealthManagerUI.OnOverHealth += IconHealthManagerUI_OnOverHealth;
		iconPeopleManagerUI.OnPeopleCorrectHome += messageManagerUI.CheckCorrect;
		tryAgainUI.OnTryAgain += iconPeopleManagerUI.FinishMap;
		tryAgainUI.OnTryAgain += iconHomeManagerUI.FinishMap;
		tryAgainUI.OnTryAgain += messageManagerUI.HideAllLine;
		winUI.OnHideLine += messageManagerUI.HideAllLine;
		
		tryAgainUI.gameObject.SetActive(false);
		winUI.gameObject.SetActive(false);
	}

	private void IconHealthManagerUI_OnOverHealth() {
		tryAgainUI.Show();
	}

	public IconHomeManagerUI GetIconHomeManagerUI() {
		return iconHomeManagerUI;
	}
	
	public IconPeopleManagerUI GetIconPeopleManagerUI() {
		return iconPeopleManagerUI;
	}
	
	public IconHealthManagerUI GetIconHealthManagerUI() {
		return iconHealthManagerUI;
	}

	public MessageManagerUI GetMessageManagerUI() {
		return messageManagerUI;
	}

	public void ShowUIWin() {
		// show trong 0.6s đợi message fade
		winUI.Show(0.75f, 0.6f);
	}
}
