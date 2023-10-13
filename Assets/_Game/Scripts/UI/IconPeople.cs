using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconPeople : MonoBehaviour, IPointerDownHandler, IPointerUpHandler {
	public Action OnIncorrect;
	public Action<PeopleSO> OnCorrectTarget;

	[Header("Element")] [SerializeField] private Image imgAvatar;
	[SerializeField] private TMP_Text txtName;
	[SerializeField] private LayoutElement layoutElement;
	[SerializeField] private RectTransform rect;

	private float delta = 50;
	private Vector2 offset = new Vector2(0, 100);
	private Vector2 originLocalPosition;
	private Vector2 originAnchor;
	private bool isSelected = false;
	private PeopleSO data;
	private List<IconHome> targets;

	public void Init(List<IconHome> targets, PeopleSO data) {
		this.targets = targets;
		this.data = data;
		imgAvatar.sprite = data.avatar;
		txtName.text = data.name;
	}

	private void Update() {
		if (!isSelected) return;
		transform.position = Input.GetTouch(0).position + offset;
	}


	public void OnPointerDown(PointerEventData eventData) {
		isSelected = true;
		EnableIgnoreLayout(true);
		originLocalPosition = rect.localPosition;
		originAnchor = rect.anchoredPosition;
	}

	private void ComeBar() {
		rect.localPosition = originLocalPosition;
		rect.anchoredPosition = originAnchor;
		EnableIgnoreLayout(false);
	}

	public void EnableIgnoreLayout(bool enable) {
		layoutElement.ignoreLayout = enable;
	}

	public IconHome currentTarget { get; private set; }

	public void OnPointerUp(PointerEventData eventData) {
		isSelected = false;

		currentTarget = CheckAllTarget();
		if (currentTarget) {
			ComHome();
		}
		else {
			if (CheckMergeIncorrect()) {
				OnIncorrect?.Invoke();
			}

			ComeBar();
		}
	}

	public void ComHome(bool isSetUp = false) {
		if(!isSetUp) SFX.Instance.PlayCorrect();
		if (currentTarget == null) {
			currentTarget = targets[0];
		}
		
		rect.SetParent(currentTarget.transform);
		rect.anchorMin = Vector2.one * 0.5f;
		rect.anchorMax = Vector2.one * 0.5f;
		rect.pivot = Vector2.one;
		rect.anchoredPosition = rect.sizeDelta / 2;
		OnCorrectTarget?.Invoke(data);
	}

	private IconHome CheckAllTarget() {
		foreach (var icon in targets) {
			float x = Vector2.Distance(transform.position, icon.transform.position);
			if (x < delta) return icon;
		}

		return null;
	}

	private bool CheckMergeIncorrect() {
		foreach (IconHome icon in iconHomes) {
			float x = Vector2.Distance(transform.position, icon.transform.position);
			if (x < delta) {
				return true;
			}
		}

		return false;
	}

	private List<IconHome> iconHomes;

	public void SetAllHomes(List<IconHome> iconHomes) {
		this.iconHomes = new List<IconHome>(iconHomes);
		foreach (var target in targets) {
			this.iconHomes.Remove(target);
		}
	}

	public void RemoveTarget(IconHome target) {
		targets.Remove(target);
	}

	public void RemoveAllAction() {
		OnIncorrect = null;
		OnCorrectTarget = null;
	}

	public void RemoveHome(IconHome iconCurrentTarget) {
		this.iconHomes.Remove(iconCurrentTarget);
	}
}