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
		for (int i = amountPeople - 1; i >= 0 ; i--) {
			pooledObjects[i].EnableIgnoreLayout(false);
			pooledObjects[i].RemoveAllAction();
			ResetSetupPoolObject(pooledObjects[i]);
			pooledObjects[i].transform.SetAsFirstSibling();
		}
	}

	public void Add(List<IconHome> targets, MapManager.Map map, int startIndexPeople) {
		List<PeopleSO> peoplesSO = map.GetPeoplesSO();
		int index = 0;
		iconDoneHome = new List<IconPeople>();
		for (int i = startIndexPeople; i < startIndexPeople + peoplesSO.Count; i++) {
			IconPeople icon = GetObjectPooledAvailable();
			icon.Init(targets, peoplesSO[index]);
			
			icon.OnCorrectTarget += (x) => {
				OnPeopleCorrectHome?.Invoke(x);
				for (int j = 0; j < amountPeople; j++) {
					pooledObjects[j].RemoveTarget(icon.currentTarget);
					pooledObjects[j].RemoveHome(icon.currentTarget);
				}
				
				ModeFindCatManager.Instance.GetCurrentMapManager().SetCorrectTarget(x);
			};
			
			icon.OnIncorrect += ()=>
			{
				ModeFindCatManager.Instance.Incorrent();
				SFX.Instance.PlayIncorrect();
			};
			
			icon.gameObject.SetActive(true);
			if (map.peoples[index].isComeHome) {
				iconDoneHome.Add(icon);
			}
			
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

	private List<IconPeople> iconDoneHome;
	public List<IconPeople> GetIconDoneHome() {
		return iconDoneHome;
	}
	
	public void EnableRaycastTargetIconPeople(bool enable) {
		foreach (var icon in pooledObjects) {
			icon.EnableRaycastTargetIconPeople(enable);
		}
	}

	public List<IconPeople> Icons => pooledObjects;
}
