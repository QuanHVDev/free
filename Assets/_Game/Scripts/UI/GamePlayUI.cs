using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GamePlayUI : MonoBehaviour
{
	[SerializeField] private IconHomeManagerUI iconHomeManagerUI;
	[SerializeField] private IconPeopleManagerUI iconPeopleManagerUI;
	[SerializeField] private IconHealthManagerUI iconHealthManagerUI;
	[SerializeField] private TryAgainUI tryAgainUI;
	[SerializeField] private MessageManagerUI messageManagerUI;
	[SerializeField] private WinUI winUI;
	[SerializeField] private TutorialUI tutorialUI;
	[SerializeField] private ProcessBar processBar;
	[SerializeField] private TMP_Text txtLevel;
	[SerializeField] private Button btnSkip;
	public Button BtnSkip => btnSkip;

	private void Start() {
		iconHealthManagerUI.OnOverHealth += IconHealthManagerUI_OnOverHealth;
		iconPeopleManagerUI.OnPeopleCorrectHome += messageManagerUI.CheckCorrect;
		tryAgainUI.OnTryAgain += iconPeopleManagerUI.FinishMap;
		tryAgainUI.OnTryAgain += iconHomeManagerUI.FinishMap;
		tryAgainUI.OnTryAgain += messageManagerUI.HideAllLine;
		winUI.OnHideLine += messageManagerUI.HideAllLine;
		btnSkip.onClick.AddListener(() =>
		{
			GameManager.Instance.DoSkip();
			btnSkip.gameObject.SetActive(false);
		});
		
		tryAgainUI.gameObject.SetActive(false);
		winUI.gameObject.SetActive(false);
		btnSkip.gameObject.SetActive(false);
		
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
	
	public void EnableRaycastTargetIconPeople(bool enable) {
		iconPeopleManagerUI.EnableRaycastTargetIconPeople(enable);
	}

	public void SetSmoothBar(float newPercent) {
		processBar.SetSmooth(newPercent);
	}

	public void ShowUISwipe(bool enable)
	{
		tutorialUI.ShowSwipe(enable);
	}

	public void ShowHintObj(Transform target, Action OnComplete)
	{
		tutorialUI.HintObject(target, OnComplete);
	}

	public void SetHint(Transform start, Transform end)
	{
		tutorialUI.SetupHint(start, end);
		tutorialUI.ShowHint();
	}

	public void TurnOffTutorialUI()
	{
		tutorialUI.gameObject.SetActive(false);
	}

	public void UpdateTitle(int level, int step)
	{
		txtLevel.text = $"Level {level + 1}-{step+1}";
	}

	public void ShowButtonSkip(bool enable)
	{
		btnSkip.gameObject.SetActive(enable);
	}
}
