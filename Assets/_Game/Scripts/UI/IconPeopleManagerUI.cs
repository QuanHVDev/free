using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IconPeopleManagerUI : MonoBehaviour {
	public Action<PeopleSO> OnPeopleCorrectHome;

	[SerializeField] private IconPeople iconPeople;
	[SerializeField] private Transform content;
	private List<IconPeople> iconPeoples;
//List<MapManager.ElementMap> homes, List<Transform> IconHomes

	public void TryAgain() {
		if (iconPeoples == null || iconPeoples.Count <= 0) return;

		foreach (var icon in iconPeoples) {
			icon.EnableIgnoreLayout(false);
			icon.transform.SetParent(content);
		}
	}

	public void Add(List<IconHome> targets, List<PeopleSO> peoplesSO, int startIndexPeople) {
		if (iconPeoples == null) {
			iconPeoples = new List<IconPeople>();
			iconPeople.gameObject.SetActive(false);
		}
		
		int index = 0;
		for (int i = startIndexPeople; i < startIndexPeople + peoplesSO.Count; i++) {
			IconPeople icon;
			if (i >= iconPeoples.Count) {
				icon = Instantiate(iconPeople, content);
				iconPeoples.Add(icon);
			}
			else {
				icon = iconPeoples[index];
			}

			icon.Init(targets, peoplesSO[index]);
			icon.OnCorrectTarget += OnPeopleCorrectHome;
			icon.OnCorrectTarget += (x) => {
				foreach (var icon in iconPeoples) {
					icon.RemoveTarget(icon.currentTarget);
				}
			};
			icon.OnCorrectTarget += GameManager.Instance.GetCurrentMapManager().SetCorrectTarget;
			icon.OnIncorrect += GameManager.Instance.Incorrent;
			icon.OnIncorrect += SFX.Instance.PlayIncorrect;
			icon.gameObject.SetActive(true);
			index++;
		}
	}

	public void SetAllHomesForIcon(List<IconHome> iconHomes) {
		foreach (var icon in iconPeoples) {
			icon.SetAllHomes(iconHomes);
		}
	}
}
