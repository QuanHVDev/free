using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel;
	
	[Serializable]
	public class ElementMap {
		public Transform home;
		public List<PeopleSO> peoples;
		public bool isComeHome = false;
	}
	
	[Serializable]
	public class ElementMessage {
		public List<PeopleSO> peoples;
		public String message;
	}

	[SerializeField] private List<ElementMap> homes;
	[SerializeField] private List<ElementMessage> messagesForHint;

	public void SetCorrectTarget(PeopleSO target) {
		foreach (var e in homes) {
			if (e.peoples.Contains(target)) {
				e.isComeHome = true;
				break;
			}
		}

		if (CheckDoneAllHome()) {
			OnFinishLevel?.Invoke();
		}
	}

	private bool CheckDoneAllHome() {
		foreach (var e in homes) {
			if (!e.isComeHome) {
				return false;
			}
		}

		return true;
	}

	public List<ElementMap> GetHomes() {
		return homes;
	}

	public List<ElementMessage> GetMessagesForHint() {
		return messagesForHint;
	}
}