using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class IconPeopleManagerUI : PoolingManagerBase<IconPeopleManagerUI, IconPeople> {
	public Action<PeopleSO> OnPeopleCorrectHome;
	private int amountPeople;

	public void FinishMap() {
		if (pooledObjects == null || pooledObjects.Count <= 0) return;

		foreach (var icon in pooledObjects) {
			icon.EnableIgnoreLayout(false);
			icon.transform.SetParent(content);
			icon.gameObject.SetActive(false);
			icon.RemoveAllAction();

			ResetSetupPoolObject(icon);
		}
	}

	public void Add(List<IconHome> targets, List<PeopleSO> peoplesSO, int startIndexPeople) {
		int index = 0;
		for (int i = startIndexPeople; i < startIndexPeople + peoplesSO.Count; i++) {
			IconPeople icon = GetObjectPooledAvailable();
			icon.Init(targets, peoplesSO[index]);
			
			icon.OnCorrectTarget += OnPeopleCorrectHome;
			icon.OnCorrectTarget += (x) => {
				for (int j = 0; j < amountPeople; j++) {
					pooledObjects[j].RemoveTarget(icon.currentTarget);
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
		for (int j = 0; j < amountPeople; j++) {
			pooledObjects[j].SetAllHomes(iconHomes);
		}
	}

	public void SetAmountPeople(int amountPeople) {
		this.amountPeople = amountPeople;
	}
}
