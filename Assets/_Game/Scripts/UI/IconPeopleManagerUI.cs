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
	public void Init() {
		iconPeoples = new List<IconPeople>();
		iconPeople.gameObject.SetActive(false);
	}

	public void TryAgain() {
		if (iconPeoples == null || iconPeoples.Count <= 0) return;

		foreach (var icon in iconPeoples) {
			Destroy(icon.gameObject);
		}
	}

	public void Add(List<Transform> targets, List<PeopleSO> peoplesSO) {

		foreach (var peopleSO in peoplesSO) {
			var icon = Instantiate(iconPeople, content);

			icon.Init(targets, peopleSO);
			

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
			iconPeoples.Add(icon);
		}
	}

	public void SetAllHomesForIcon(List<Transform> iconHomes) {
		foreach (var icon in iconPeoples) {
			icon.SetAllHomes(iconHomes);
		}
	}
}
