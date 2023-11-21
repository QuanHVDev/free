using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHomeManagerUI : PoolingManagerBase<IconHomeManagerUI, IconHome> {
	[SerializeField] private IconHome parentHomeHor;
	private List<IconHome> currentLevelIconHome;
	private int amount;
	public void Add(MapManager mapManager, GamePlayUI gamePlayUI) {
		int startIndexPeople = 0;
		
		List<IconPeople> iconDoneHome = new List<IconPeople>();
		currentLevelIconHome = new List<IconHome>();
		for (int i = 0; i <  mapManager.GetHomes().Count; i++) {
			var e = mapManager.GetHomes()[i];
			Transform parent = content;

			if (e.peoples.Count > 1) {
				var obj = Instantiate(parentHomeHor, transform);
				currentLevelIconHome.Add(obj);
				obj.SetHomeModel(e.target);
				parent = obj.transform;
				parent.position = Camera.main.WorldToScreenPoint(e.target.position);
				parent.gameObject.SetActive(true);
			}

			List<IconHome> targets = new List<IconHome>();
			for (int j = startIndexPeople; j < startIndexPeople + e.peoples.Count; j++) {
				IconHome icon = GetObjectPooledAvailable();
				icon.transform.SetParent(parent);

				icon.transform.position = Camera.main.WorldToScreenPoint(e.target.position);
				icon.SetHomeModel(e.target);
				icon.gameObject.SetActive(true);
				targets.Add(icon);
				if(e.peoples.Count < 2) currentLevelIconHome.Add(icon);
			}
			
			gamePlayUI.GetIconPeopleManagerUI().Add(targets, e, startIndexPeople);
			foreach (var icon in gamePlayUI.GetIconPeopleManagerUI().GetIconDoneHome()) {
				iconDoneHome.Add(icon);
				currentLevelIconHome.Remove(icon.currentTarget);
			}
			
			startIndexPeople += e.peoples.Count;
		}
		
		// Call Action with icons set came home
		foreach (var icon in iconDoneHome) {
			icon.ComeHome(true);
		}

		gamePlayUI.GetIconPeopleManagerUI().SetAmountPeople(startIndexPeople);
		parentHomeHor.gameObject.SetActive(false);
		
		amount = startIndexPeople;
	}

	private void FixedUpdate() {
		if (currentLevelIconHome != null)
		{
			foreach (var icon in currentLevelIconHome) {
				icon.transform.position = Camera.main.WorldToScreenPoint(icon.homeModel.position);
			}
		}
	}

	public void FinishMap() {
		for (int i = 0; i < amount; i++) {
			ResetSetupPoolObject(pooledObjects[i]);
		}

		currentLevelIconHome = new List<IconHome>();
	}

	public List<IconHome> GetCurrentIconForMap() {
		List<IconHome> listCurrentIcon = new List<IconHome>();
		for (int i = 0; i < amount; i++) {
			listCurrentIcon.Add(pooledObjects[i]);
		}

		return listCurrentIcon;
	}
}