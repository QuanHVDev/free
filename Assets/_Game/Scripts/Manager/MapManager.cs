using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class MapManager : MonoBehaviour {
	public Action OnFinishLevel, OnCompletePath;
	public Action<float> OnCorrect;
	public Action<CameraManager.NameOfAnimationCamera, CameraManager.StateVirtualCamera> OnCameraLookTarget;
	public Func<Transform, Transform, CameraManager.ElementCamera> OnSetCatTarget;
	public Action<bool> OnMapBusy;

	[FormerlySerializedAs("currentIndexMap")] public int currentIndexStep = 0;
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
	private PeopleSO catSO;

	public ElementMap MapInfo { get; private set; }

	private void Start() {
		dataNavInstance = NavMesh.AddNavMeshData(dataNav);
	}

	public void InitData(int step = -1) {
		if (step != -1) {
			currentIndexStep = step;
		}
		
		MapInfo = new ElementMap(maps[currentIndexStep]);
		cats = new List<Transform>();
		
		int countCat = 0;
		foreach (var map in MapInfo.element) {
			foreach (var elementPeople in map.peoples) {
				if (!elementPeople.isComeHome) {
					countCat++;
				}
			}
		}

		CountCatMoved = 0;
		SetMaxCatNeedMove(countCat);
		OnCorrect?.Invoke(CountCatMoved * 1.0f / MaxCatNeedMove);
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

	public int MaxCatNeedMove { get; private set; } = 0;
	public int CountCatMoved { get; private set; } = 0;

	private void SetMaxCatNeedMove(int value) {
		MaxCatNeedMove = value;
	}

	private IEnumerator SetCorrectTargetAsync(PeopleSO peopleSO) {
		foreach (var e in MapInfo.element) {
			foreach (var house in e.peoples) {
				if (house.people == peopleSO) {
					CountCatMoved++;
					OnCorrect?.Invoke(CountCatMoved * 1.0f / MaxCatNeedMove);
					PrefabPeople catTarget = Instantiate(peopleSO.prefab, MapInfo.catSpawnPosition);
					catTarget.name = peopleSO.name;
					cats.Add(catTarget.transform);
					catSO = peopleSO;
					yield return SetCameraLookTargetAsync(catTarget, e.target);
					house.isComeHome = true;
					if (CheckDoneAllHome()) {
						OnFinishLevel?.Invoke();
					}
					break;
				}
			}
		}
	}

	private IEnumerator SetCameraLookTargetAsync(PrefabPeople cat, Transform positionToMove) {
		var x = OnSetCatTarget?.Invoke(cat.transform, MapInfo.cameraPosition);
		if (MapInfo.valueZoomCamera != 0) {
			GameManager.Instance.Camera.SetFOV(x, MapInfo.valueZoomCamera);
		}
		x.VirtualCamera.gameObject.SetActive(true);
		OnCameraLookTarget?.Invoke(x.triggerNameAnimationState, CameraManager.StateVirtualCamera.Wait);
		IsMapBusy = true;
		OnMapBusy?.Invoke(!IsMapBusy);
		cat.SetTargetToMove(positionToMove);
		StartCoroutine(WaitCatMoveToTarget(cat, positionToMove));
		yield return new WaitUntil(() =>
		{
			return GameManager.Instance.IsSkip || !IsMapBusy;
		});

		if (!GameManager.Instance.IsSkip)
		{
			yield return WaitCatMoveToTarget(cat, positionToMove);
			yield return new WaitForSeconds(2f);
		}

		GameManager.Instance.ResetSkip();
		IsMapBusy = false;
		OnCompletePath?.Invoke();
		OnMapBusy?.Invoke(!IsMapBusy);
		if (MapInfo.valueZoomCamera != 0) {
			GameManager.Instance.Camera.ResetFOV(x);
		}
	}

	private bool IsCatRunning = false;
	private IEnumerator WaitCatMoveToTarget(PrefabPeople cat, Transform positionToMove)
	{
		yield return new WaitUntil(() =>
		{
			bool isCame = true;
			if (cat) {
				Debug.DrawLine(cat.transform.position, positionToMove.position, Color.red);
				isCame = Mathf.Abs(cat.transform.position.x - positionToMove.position.x) <= cat.nav.stoppingDistance &&
				              Mathf.Abs(cat.transform.position.z - positionToMove.position.z) <= cat.nav.stoppingDistance;
			}
			
			if (isCame) {
				IsMapBusy = false;
			}
			
			return isCame;
		});

		yield return CatMovedTarget(cat);
	}

	private IEnumerator CatMovedTarget(PrefabPeople cat)
	{
		cat.StartRandomAnimFinishTarget();
		var targetDir = MapInfo.cameraPosition.position - cat.transform.position;
		targetDir = new Vector3(targetDir.x, 0, targetDir.z);
		
		var angleToTarget = Vector3.Angle(cat.transform.forward, targetDir);
		float originAngle = 0;
		Vector3 vector;
		cat.transform.rotation.ToAngleAxis(out originAngle, out vector);
		
		// Cho xoay thử, nếu xoay xong mà góc bé hơn thì đã xoay đúng chiều, nếu sai thì xoay ngược lại (+- 0.01)
		cat.transform.DORotate(new Vector3(0, originAngle*vector.y-0.01f, 0), 0).OnComplete(() => {
			var angleCheck = Vector3.Angle(cat.transform.forward, targetDir);
			if (angleCheck < angleToTarget) {
				cat.transform.DORotate(new Vector3(0, originAngle*vector.y-angleToTarget, 0), 1);
			}
			else {
				cat.transform.DORotate(new Vector3(0, originAngle*vector.y+angleToTarget, 0), 1);
			}
		});

		yield return null;
	}

	private bool CheckDoneAllHome() {
		foreach (var e in MapInfo.element) {
			foreach (var house in e.peoples) {
				if (!house.isComeHome) {
					return false;
				}
			}
		}

		return true;
	}

	public List<Map> GetHomes() {
		return MapInfo.element;
	}

	public List<ElementMessage> GetMessagesForHint() {
		return MapInfo.messagesForHint;
	}

	public int GetCountMaps() {
		return maps.Count;
	}

	public Transform GetCurrentCameraPosition() {
		return MapInfo.cameraPosition;
	}

	public void RemoveCurrentNavmeshData() {
		NavMesh.RemoveNavMeshData(dataNavInstance);
	}

	private List<Transform> cats;

	public void DeleteAllCats()
	{
		StartCoroutine(DeleteAllCatsAsync());
	}

	private IEnumerator DeleteAllCatsAsync()
	{
		if (cats == null || cats.Count == 0) yield return null;

		List<ParticleSystem> vfxs = new List<ParticleSystem>();
		List<Transform> catsClone = new List<Transform>(cats);
		foreach (Transform transform in catsClone)
		{
			var vfx = Instantiate(catSO.vfxHide, transform.position, Quaternion.identity);
			vfx.Play();
			vfxs.Add(vfx);
		}
		
		foreach (Transform trans in catsClone)
		{
			Destroy(trans.gameObject);
		}
		
		yield return new WaitForSeconds(0.3f);
		foreach (ParticleSystem vfx in vfxs)
		{
			Destroy(vfx.gameObject);
		}
		
	}
}