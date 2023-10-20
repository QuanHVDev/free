using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel;
	public int currentIndexMap = 0;
	
	[Serializable]
	public class ElementMap {
		public List<Map> element;
		public List<ElementMessage> messagesForHint;
		public Transform cameraPosition;
	}

	[SerializeField] private List<ElementMap> maps;

	[Serializable]
	public class Map {
		public Transform target;
		public List<ElementPeople> peoples;

		public List<PeopleSO> GetPeoplesSO() {
			List<PeopleSO> list = new List<PeopleSO>();
			foreach (var e in peoples) {
				list.Add(e.people);
			}

			return list;
		}
	}

	[Serializable]
	public class ElementPeople {
		public PeopleSO people;
		public bool isComeHome = false;
	}
	
	[Serializable]
	public class ElementMessage {
		public List<PeopleSO> peoples;
		public String message;
	}


	public void SetCorrectTarget(PeopleSO peopleSO) {
		foreach (var e in maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					house.isComeHome = true;
					Instantiate(peopleSO.prefab, e.target);
					break;
				}
			}
		}

		if (CheckDoneAllHome()) {
			OnFinishLevel?.Invoke();
		}
	}

	private bool CheckDoneAllHome() {
		foreach (var e in maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (!house.isComeHome) {
					return false;
				}
			}
		}

		return true;
	}

	public List<Map> GetHomes() {
		return maps[currentIndexMap].element;
	}

	public List<ElementMessage> GetMessagesForHint() {
		return maps[currentIndexMap].messagesForHint;
	}

	public int GetCountMaps() {
		return maps.Count;
	}

	public Transform GetCurrentCameraPosition() {
		return maps[currentIndexMap].cameraPosition;
	}
}