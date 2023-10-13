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

	private float delta = 50;
	private Vector2 offset = new Vector2(0, 100);
	private Vector2 originLocalPosition;
	private Vector2 originAnchor;
	private bool isSelected;
	private RectTransform rect;
	private PeopleSO data;
	private List<Transform> targets;

	public void Init(List<Transform> targets, PeopleSO data) {
		this.targets = targets;
		this.data = data;
		imgAvatar.sprite = data.avatar;
		txtName.text = data.name;
	}

	private void Start() {
		isSelected = false;
		rect = transform.GetComponent<RectTransform>();
	}

	private void Update() {
		if (!isSelected) return;
		transform.position = Input.GetTouch(0).position + offset;
	}


	public void OnPointerDown(PointerEventData eventData) {
		isSelected = true;
		layoutElement.ignoreLayout = true;
		originLocalPosition = rect.localPosition;
		originAnchor = rect.anchoredPosition;
	}

	private void ComeHome() {
		rect.localPosition = originLocalPosition;
		rect.anchoredPosition = originAnchor;
		layoutElement.ignoreLayout = false;
	}

	public Transform currentTarget { get; private set; }

	public void OnPointerUp(PointerEventData eventData) {
		isSelected = false;

		Transform currentTarget = CheckAllTarget();
		if (currentTarget) {
			rect.SetParent(currentTarget);
			rect.anchorMin = Vector2.one * 0.5f;
			rect.anchorMax = Vector2.one * 0.5f;
			rect.pivot = Vector2.one;
			rect.anchoredPosition = rect.sizeDelta / 2;

			SFX.Instance.PlayCorrect();
			OnCorrectTarget?.Invoke(data);
		}
		else {
			if (CheckMergeIncorrect()) {
				OnIncorrect?.Invoke();
			}

			ComeHome();
		}
	}

	private Transform CheckAllTarget() {
		foreach (var trans in targets) {
			float x = Vector2.Distance(transform.position, trans.position);
			if (x < delta) return trans;
		}

		return null;
	}

	private bool CheckMergeIncorrect() {
		foreach (Transform trans in iconHomes) {
			float x = Vector2.Distance(transform.position, trans.position);
			if (x < delta) {
				return true;
			}
		}

		return false;
	}

	private List<Transform> iconHomes;

	public void SetAllHomes(List<Transform> iconHomes) {
		this.iconHomes = new List<Transform>(iconHomes);
		foreach (var target in targets) {
			this.iconHomes.Remove(target);
		}
	}

	public void RemoveTarget(Transform target) {
		targets.Remove(target);
	}
}