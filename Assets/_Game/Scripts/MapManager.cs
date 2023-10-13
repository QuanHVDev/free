using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel;
	
	[Serializable]
	public class ElementMap {
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

	[SerializeField] private List<ElementMap> element;
	[SerializeField] private List<ElementMessage> messagesForHint;

	public void SetCorrectTarget(PeopleSO peopleSO) {
		foreach (var e in element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					house.isComeHome = true;
					break;
				}
			}
		}

		if (CheckDoneAllHome()) {
			OnFinishLevel?.Invoke();
		}
	}

	private bool CheckDoneAllHome() {
		foreach (var e in element) {
			foreach (var house in e.peoples) {
				if (!house.isComeHome) {
					return false;
				}
			}
		}

		return true;
	}

	public List<ElementMap> GetHomes() {
		return element;
	}

	public List<ElementMessage> GetMessagesForHint() {
		return messagesForHint;
	}
}