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
	public Action<bool> OnMapBusy;

	public int currentIndexMap = 0;
	public bool IsMapBusy { get; private set; } = false;

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
		NavMesh.RemoveAllNavMeshData();
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
		StartCoroutine(SetCorrectTargetAsync(peopleSO));
	}

	private IEnumerator SetCorrectTargetAsync(PeopleSO peopleSO) {
		foreach (var e in maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					catTarget = Instantiate(peopleSO.prefab, maps[currentIndexMap].catSpawnPosition).transform; 
					yield return LookTargetAsync(peopleSO, e.target);
					house.isComeHome = true;
					if (CheckDoneAllHome()) {
						OnFinishLevel?.Invoke();
					}
					break;
				}
			}
		}
	}

	private IEnumerator LookTargetAsync(PeopleSO data, Transform positionToMove) {
		var x = OnSetCatTarget?.Invoke(catTarget, maps[currentIndexMap].cameraPosition);
		x.VirtualCamera.gameObject.SetActive(true);
		OnCameraLookTarget?.Invoke(x.triggerNameAnimationState.ToString(), CameraManager.StateVirtualCamera.Wait);
		if (catTarget.TryGetComponent(out NavMeshAgent nav)) {
			IsMapBusy = true;
			OnMapBusy?.Invoke(!IsMapBusy);
			nav.SetDestination(positionToMove.position);
			yield return new WaitUntil(() => {
				Debug.DrawLine(catTarget.position, positionToMove.position, Color.red);
				bool isCame = Mathf.Abs(catTarget.position.x - positionToMove.position.x) <= 0.3f &&
				              Mathf.Abs(catTarget.position.z - positionToMove.position.z) <= 0.3f;
				if (isCame) {
					IsMapBusy = false;
					OnMapBusy?.Invoke(!IsMapBusy);
				}
				return isCame;
			});
		}

		x.VirtualCamera.gameObject.SetActive(false);
		var vfx = Instantiate(data.vfxHide, catTarget.transform.position, Quaternion.identity);
		vfx.Play();
		yield return new WaitForSeconds(0.1f);
		catTarget.gameObject.SetActive(false);
		OnCompletePath?.Invoke();

		yield return new WaitForSeconds(0.5f);
		Destroy(vfx.gameObject);
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