using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel, OnCompletePath;
	public Action<CameraManager.NameOfAnimationCamera, CameraManager.StateVirtualCamera> OnCameraLookTarget;
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
		public float valueZoomCamera = 0;

		public ElementMap(ElementMap e) {
			element = new List<Map>();
			e.element.ForEach((item) =>
			{
				element.Add(new Map(item));
			});
			
			messagesForHint = new List<ElementMessage>();
			e.messagesForHint.ForEach((item) => {
				messagesForHint.Add(new ElementMessage(item));
			});

			cameraPosition = e.cameraPosition;
			catSpawnPosition = e.catSpawnPosition;
			valueZoomCamera = e.valueZoomCamera;
		}
	}

	[SerializeField] private List<ElementMap> maps;
	[SerializeField] private NavMeshData dataNav;
	private NavMeshDataInstance dataNavInstance;
	private PrefabPeople catTarget;

	public List<ElementMap> Maps { get; private set; }

	private void Start() {
		dataNavInstance = NavMesh.AddNavMeshData(dataNav);
	}

	public void InitData() {
		Maps = new List<ElementMap>();
		maps.ForEach((item) =>
		{
			Maps.Add(new ElementMap(item));
		});
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

		public Map(Map map) {
			target = map.target;
			peoples = new List<ElementPeople>();
			map.peoples.ForEach((x) => {
				var people = new ElementPeople();
				people.people = x.people;
				people.isComeHome = x.isComeHome;
				peoples.Add(people);
			});
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

		public ElementMessage(ElementMessage e) {
			peoples = new List<PeopleSO>();
			e.peoples.ForEach((item) => {
				peoples.Add(item);
			});
			
			message = e.message;
		}
	}


	#endregion
	

	public void SetCorrectTarget(PeopleSO peopleSO) {
		StartCoroutine(SetCorrectTargetAsync(peopleSO));
	}

	private IEnumerator SetCorrectTargetAsync(PeopleSO peopleSO) {
		foreach (var e in Maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					catTarget = Instantiate(peopleSO.prefab, Maps[currentIndexMap].catSpawnPosition); 
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
		var x = OnSetCatTarget?.Invoke(catTarget.transform, Maps[currentIndexMap].cameraPosition);
		if (Maps[currentIndexMap].valueZoomCamera != 0) {
			GameManager.Instance.Camera.SetFOV(x, Maps[currentIndexMap].valueZoomCamera);
		}
		x.VirtualCamera.gameObject.SetActive(true);
		OnCameraLookTarget?.Invoke(x.triggerNameAnimationState, CameraManager.StateVirtualCamera.Wait);
		if (catTarget.TryGetComponent(out NavMeshAgent nav)) {
			IsMapBusy = true;
			OnMapBusy?.Invoke(!IsMapBusy);
			catTarget.SetTargetToMove(positionToMove);
			yield return new WaitUntil(() => {
				Debug.DrawLine(catTarget.transform.position, positionToMove.position, Color.red);
				bool isCame = Mathf.Abs(catTarget.transform.position.x - positionToMove.position.x) <= nav.stoppingDistance &&
				              Mathf.Abs(catTarget.transform.position.z - positionToMove.position.z) <= nav.stoppingDistance;
				if (isCame) {
					IsMapBusy = false;
					OnMapBusy?.Invoke(!IsMapBusy);
				}
				return isCame;
			});
			
		}
		catTarget.StartRandomAnimFinishTarget();
		yield return new WaitForSeconds(2f);
		OnCompletePath?.Invoke();
		if (Maps[currentIndexMap].valueZoomCamera != 0) {
			GameManager.Instance.Camera.ResetFOV(x);
		}
		//var vfx = Instantiate(data.vfxHide, catTarget.transform.position, Quaternion.identity);
		//vfx.Play();
		
		

		//yield return new WaitForSeconds(0.5f);
		//Destroy(vfx.gameObject);
	}

	private bool CheckDoneAllHome() {
		foreach (var e in Maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (!house.isComeHome) {
					return false;
				}
			}
		}

		return true;
	}

	public List<Map> GetHomes() {
		return Maps[currentIndexMap].element;
	}

	public List<ElementMessage> GetMessagesForHint() {
		return Maps[currentIndexMap].messagesForHint;
	}

	public int GetCountMaps() {
		return maps.Count;
	}

	public Transform GetCurrentCameraPosition() {
		return Maps[currentIndexMap].cameraPosition;
	}

	public void RemoveCurrentNavmeshData() {
		NavMesh.RemoveNavMeshData(dataNavInstance);
	}
}