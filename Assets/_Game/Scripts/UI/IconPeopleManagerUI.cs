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
		for (int i = 0; i < amountPeople; i++) {
			pooledObjects[i].EnableIgnoreLayout(false);
			pooledObjects[i].RemoveAllAction();
			ResetSetupPoolObject(pooledObjects[i]);
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
					pooledObjects[j].RemoveHome(icon.currentTarget);
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
