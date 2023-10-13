using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconHomeManagerUI : PoolingManagerBase<IconHomeManagerUI, IconHome> {
	[SerializeField] private GameObject parentHomeHor;

	private int amount;
	public void Add(MapManager mapManager, GamePlayUI gamePlayUI) {
		int startIndexPeople = 0;
		for (int i = 0; i <  mapManager.GetHomes().Count; i++) {
			var e = mapManager.GetHomes()[i];
			Transform parent = content;

			if (e.peoples.Count > 1) {
				parent = Instantiate(parentHomeHor, transform).transform;
				parent.position = Camera.main.WorldToScreenPoint(e.home.transform.position);
				parent.gameObject.SetActive(true);
			}

			List<IconHome> targets = new List<IconHome>();
			for (int j = startIndexPeople; j < startIndexPeople + e.peoples.Count; j++) {
				IconHome icon = GetObjectPooledAvailable();
				icon.transform.SetParent(parent);

				icon.transform.position = Camera.main.WorldToScreenPoint(e.home.transform.position);
				icon.gameObject.SetActive(true);
				targets.Add(icon);
			}

			gamePlayUI.GetIconPeopleManagerUI().Add(targets, e.peoples, startIndexPeople);
			startIndexPeople += e.peoples.Count;
		}

		gamePlayUI.GetIconPeopleManagerUI().SetAmountPeople(startIndexPeople);
		parentHomeHor.SetActive(false);
		
		amount = startIndexPeople;
	}

	public void FinishMap() {
		for (int i = 0; i < amount; i++) {
			ResetSetupPoolObject(pooledObjects[i]);
		}
	}

	public List<IconHome> GetCurrentIconForMap() {
		List<IconHome> listCurrentIcon = new List<IconHome>();
		for (int i = 0; i < amount; i++) {
			listCurrentIcon.Add(pooledObjects[i]);
		}

		return listCurrentIcon;
	}
}