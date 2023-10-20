using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraManager : MonoBehaviour {
	private const string ANIMATION_SHOWALL = "switchCamShowAll";

	[SerializeField] private CinemachineStateDrivenCamera _drivenCamera;
	[SerializeField] private CinemachineVirtualCamera virtualCameraShowAll;
	[SerializeField] private Animator cameraAnim;


	public List<ElementCamera> virtualCameraElements;

	[Serializable]
	public class ElementCamera {
		public CinemachineVirtualCamera VirtualCamera;
		public bool IsCanChangePosition = true;
		public string triggerNameAnimationState;
	}

	[SerializeField] private LayerMask mapLayer;
	[SerializeField] private Vector3 OriginPosition;
	[SerializeField] private float leftLimit = -12, rightLimit = 12, botLimit = -10, topLimit = 10;

	private Vector2 firstPosition;
	public bool debug;
	private ElementCamera elementCameraPrev;

	private enum StateMouse {
		wait,
		move,
		inUI
	}

	//private StateMouse state;

	private void Start() {
		Input.multiTouchEnabled = false;
	}

	public ElementCamera GetVirtualCameraFree(Transform parent) {
		foreach (var e in virtualCameraElements) {
			if ((elementCameraPrev != null && e == elementCameraPrev) || !e.IsCanChangePosition) {
				continue;
			}

			e.VirtualCamera.transform.SetParent(parent);
			e.VirtualCamera.transform.localPosition = Vector3.zero;
			e.VirtualCamera.transform.localRotation = Quaternion.Euler(Vector3.zero);
			elementCameraPrev = e;
			return e;
		}

		return null;
	}

	public void MoveCameraToVirtualCamera(ElementCamera elementCamera) {
		SetTriggerWith(elementCamera.triggerNameAnimationState);
	}

	[ContextMenu("ReturnVirtualCameraToOrigin")]
	public void ReturnVirtualCameraToOrigin() {
		
		StartCoroutine(WaitCameraMovePositionAsync());
	}

	private void SetTriggerWith(string triggerName) {
		state = StateVirtualCamera.Move;
		cameraAnim.SetTrigger(triggerName);
	}

	public enum StateVirtualCamera {
		Wait,
		Move,
		Finish
	}

	public StateVirtualCamera state { get; private set; }


	private IEnumerator WaitCameraMovePositionAsync() {
		for (int i = 0; i < _drivenCamera.m_Instructions.Length; i++) {
			if (!virtualCameraElements[i].IsCanChangePosition) continue;
			
			virtualCameraElements[i].VirtualCamera.transform.SetParent(transform);
			_drivenCamera.m_Instructions[i].m_VirtualCamera = virtualCameraElements[i].VirtualCamera;
		}
		
		SetTriggerWith(ANIMATION_SHOWALL);
		yield return new WaitUntil(() => {
			if (Mathf.Abs(cameraAnim.gameObject.transform.position.x -
			              virtualCameraShowAll.transform.position.x) <= Mathf.Epsilon &&
			    Mathf.Abs(cameraAnim.gameObject.transform.position.y -
			              virtualCameraShowAll.transform.position.y) <= Mathf.Epsilon &&
			    Mathf.Abs(cameraAnim.gameObject.transform.position.z -
			              virtualCameraShowAll.transform.position.z) <= Mathf.Epsilon) 
			{
				state = StateVirtualCamera.Finish;
				return true;
			}

			return false;
		});
	}

	private void Update() {
	}

	// private void Update() {
	// 	if (Input.touchCount == 1) {
	// 		if (state == StateMouse.inUI) return;
	// 		Touch t = Input.GetTouch(0);
	// 		if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && state != StateMouse.move) {
	// 			Debug.Log("in UI" + t.phase);
	// 			state = StateMouse.inUI;
	// 			return;
	// 		}
	//
	// 		Debug.Log("in Object" + t.phase);
	// 		if (t.phase == TouchPhase.Began) {
	// 			state = StateMouse.move;
	// 		}
	//
	// 		Ray ray = Camera.main.ScreenPointToRay(t.position);
	// 		if (Physics.Raycast(ray, out RaycastHit hit, 999f, mapLayer) && firstPosition == Vector2.zero) {
	// 			firstPosition = t.position;
	// 			return;
	// 		}
	//
	// 		MoveCameraWith(t.position - firstPosition);
	// 		firstPosition = t.position;
	// 	}
	// 	else {
	// 		firstPosition = Vector2.zero;
	// 		state = StateMouse.wait;
	// 	}
	// }
	//
	//
	// private void MoveCameraWith(Vector2 deltaPosition) {
	// 	transform.position -= new Vector3(deltaPosition.x, 0, deltaPosition.y) * Time.deltaTime;
	// 	state = StateMouse.move;
	//
	// 	transform.position = new Vector3(
	// 		Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
	// 		0,
	// 		Mathf.Clamp(transform.position.z, botLimit, topLimit)
	// 	);
	// }
	//
	// public void ResetToOriginPosition() {
	// 	transform.position = OriginPosition;
	// }
}