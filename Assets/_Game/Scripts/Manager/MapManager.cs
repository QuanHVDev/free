using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel, OnCompletePath;
	public Action<string, CameraManager.StateVirtualCamera> OnCameraLookTarget;
	public Func<Transform, Transform, CameraManager.ElementCamera> OnSetCatTarget;

	public int currentIndexMap = 0;
	
	[Serializable]
	public class ElementMap {
		public List<Map> element;
		public List<ElementMessage> messagesForHint;
		public Transform cameraPosition;
		public Transform catSpawnPosition;
	}

	[SerializeField] private List<ElementMap> maps;
	[SerializeField] private NavMeshData dataNav;
	[SerializeField] private NavMeshDataInstance dataNavs;

	private Transform catTarget;

	private void Start() {
		NavMesh.AddNavMeshData(dataNav);
	}

	#region classElement

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


	#endregion
	

	public void SetCorrectTarget(PeopleSO peopleSO) {
		foreach (var e in maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					house.isComeHome = true;
					catTarget = Instantiate(peopleSO.prefab, maps[currentIndexMap].catSpawnPosition).transform; 
					StartCoroutine(LookTargetAsync(e.target));
					break;
				}
			}
		}

		if (CheckDoneAllHome()) {
			OnFinishLevel?.Invoke();
		}
	}

	private IEnumerator LookTargetAsync(Transform positionToMove) {
		var x = OnSetCatTarget?.Invoke(catTarget, maps[currentIndexMap].cameraPosition);
		x.VirtualCamera.gameObject.SetActive(true);
		OnCameraLookTarget?.Invoke(x.triggerNameAnimationState.ToString(), CameraManager.StateVirtualCamera.Wait);
		if(catTarget.TryGetComponent(out NavMeshAgent nav)) {
			nav.SetDestination(positionToMove.position);
			yield return new WaitUntil(() => {
				return nav.isStopped ;
			});
		}

		x.VirtualCamera.gameObject.SetActive(false);
		OnCompletePath?.Invoke();
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