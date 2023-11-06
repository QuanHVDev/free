using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel, OnCompletePath;
	public Action<float> OnCorrect;
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
		foreach (var elementMap in maps) {
			Maps.Add(new ElementMap(elementMap));
		}
		
		int countCat = 0;
		foreach (var map in Maps[currentIndexMap].element) {
			foreach (var elementPeople in map.peoples) {
				if (!elementPeople.isComeHome) {
					countCat++;
				}
			}
		}

		countCatMoved = 0;
		SetMaxCatNeedMove(countCat);
		OnCorrect?.Invoke(countCatMoved * 1.0f / maxCatNeedMove);
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

	private int maxCatNeedMove = 0, countCatMoved = 0;

	private void SetMaxCatNeedMove(int value) {
		maxCatNeedMove = value;
	}

	private IEnumerator SetCorrectTargetAsync(PeopleSO peopleSO) {
		foreach (var e in Maps[currentIndexMap].element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					countCatMoved++;
					OnCorrect?.Invoke(countCatMoved * 1.0f / maxCatNeedMove);
					catTarget = Instantiate(peopleSO.prefab, Maps[currentIndexMap].catSpawnPosition); 
					yield return SetCameraLookTargetAsync(peopleSO, e.target);
					house.isComeHome = true;
					if (CheckDoneAllHome()) {
						OnFinishLevel?.Invoke();
					}
					break;
				}
			}
		}
	}

	private IEnumerator SetCameraLookTargetAsync(PeopleSO data, Transform positionToMove) {
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
					
				}
				return isCame;
			});
			
		}
		
		catTarget.StartRandomAnimFinishTarget();
		var targetDir = Maps[currentIndexMap].cameraPosition.position - catTarget.transform.position;
		targetDir = new Vector3(targetDir.x, 0, targetDir.z);
		
		var angleToTarget = Vector3.Angle(catTarget.transform.forward, targetDir);
		float originAngle = 0;
		Vector3 vector;
		catTarget.transform.rotation.ToAngleAxis(out originAngle, out vector);
		
		// Cho xoay thử, nếu xoay xong mà góc bé hơn thì đã xoay đúng chiều, nếu sai thì xoay ngược lại (+- 0.01)
		catTarget.transform.DORotate(new Vector3(0, originAngle*vector.y-0.01f, 0), 0).OnComplete(() => {
			var angleCheck = Vector3.Angle(catTarget.transform.forward, targetDir);
			if (angleCheck < angleToTarget) {
				catTarget.transform.DORotate(new Vector3(0, originAngle*vector.y-angleToTarget, 0), 1);
			}
			else {
				catTarget.transform.DORotate(new Vector3(0, originAngle*vector.y+angleToTarget, 0), 1);
			}
		});
		
		yield return new WaitForSeconds(2f);
		OnCompletePath?.Invoke();
		OnMapBusy?.Invoke(!IsMapBusy);
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