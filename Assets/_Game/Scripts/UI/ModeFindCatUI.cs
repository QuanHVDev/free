using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModeFindCatUI : MonoBehaviour
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
	[SerializeField] private Button btnLevel;
	[SerializeField] private Button btnHome;
	[SerializeField] private ListLevelUI listLevelUI;
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
			ModeFindCatManager.Instance.DoSkip();
			btnSkip.gameObject.SetActive(false);
		});
		
		btnLevel.onClick.AddListener(() =>
		{
			listLevelUI.Show();
		});
		
		btnHome.onClick.AddListener(GoHomeAction);
		
		tryAgainUI.gameObject.SetActive(false);
		winUI.gameObject.SetActive(false);
		btnSkip.gameObject.SetActive(false);
		listLevelUI.gameObject.SetActive(false);
	}

	public void GoHomeAction()
	{
		ModeFindCatManager.Instance.CurrentMapManager.RemoveCurrentNavmeshData();
		LoadSceneUIManager.Instance.LoadMainMenu();
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

	private int currentDiamond = 0;
	public void ShowUIWin() {
		winUI.Show();
		currentDiamond = 0;
	}

	public void AddValueDiamondWinUI(int value)
	{
		StartCoroutine(AddValueDiamondWinUIAsync(value));
	}

	private IEnumerator AddValueDiamondWinUIAsync(int value)
	{
		float timeAnim = 0.03f;
		for (int i = 0; i < value; i++)
		{
			currentDiamond += 1;
			winUI.SetValueDiamond(currentDiamond, timeAnim);
			yield return new WaitForSeconds(timeAnim);
		}
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

	public void UpdateTitle()
	{
		txtLevel.text = $"Level {ModeFindCatManager.Instance.IndexMapManager + 1}-{ModeFindCatManager.Instance.CurrentMapManager.currentIndexStep + 1}";
	}

	public void ShowButtonSkip(bool enable)
	{
		btnSkip.gameObject.SetActive(enable);
	}

	public void InitListLevel(DataLevelsSO data)
	{
		listLevelUI.Init(data);
	}
}
