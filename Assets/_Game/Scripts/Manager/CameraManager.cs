using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour {

	public enum NameOfAnimationCamera {
		vCamera_1,
		vCamera_2,
		vCamera_ShowAllMap,
		vCamera_LTarget
	}
	
	
	[SerializeField] private CinemachineStateDrivenCamera _drivenCamera;
	[SerializeField] private CinemachineVirtualCamera virtualCameraShowAll;
	[SerializeField] private Animator cameraAnim;


	public List<ElementCamera> virtualCameraElements;

	[Serializable]
	public class ElementCamera {
		public CinemachineVirtualCamera VirtualCamera;
		public bool IsCanChangePosition = true;
		public NameOfAnimationCamera triggerNameAnimationState;
		public bool IsOnlyLookTarget = false;
	}

	private ElementCamera elementCameraPrev;

	private void Start() {
		Input.multiTouchEnabled = false;
	}

	public ElementCamera GetElementCameraPrev() {
		return elementCameraPrev;
	}


	public ElementCamera GetVirtualCameraFree(Transform parent) {
		foreach (var e in virtualCameraElements) {
			if ((elementCameraPrev != null && e == elementCameraPrev) || !e.IsCanChangePosition) {
				continue;
			}

			e.VirtualCamera.transform.position = parent.position;
			e.VirtualCamera.transform.rotation = parent.rotation;
			return e;
		}

		return null;
	}

	public void MoveCameraToVirtualCamera(ElementCamera elementCamera, Action OnCompele = null) {
		ChangeState(elementCamera.triggerNameAnimationState, StateVirtualCamera.Move);
		elementCamera.VirtualCamera.gameObject.SetActive(true);
		elementCameraPrev = elementCamera;
		originRotation = elementCameraPrev.VirtualCamera.transform.rotation.eulerAngles;

		if (OnCompele != null)
		{
			StartCoroutine(CheckTut(OnCompele));
		}
		
	}

	IEnumerator CheckTut(Action OnDoSomething)
	{
		yield return new WaitUntil(() => _drivenCamera.IsBlending);
		yield return new WaitWhile(() => _drivenCamera.IsBlending);
		OnDoSomething?.Invoke();
	}

	[ContextMenu("ReturnVirtualCameraToOrigin")]
	public void ReturnVirtualCameraToOrigin() {
		
		StartCoroutine(WaitCameraMovePositionAsync());
	}

	public void ChangeState(NameOfAnimationCamera triggerName, StateVirtualCamera state) {
		this.state = state;
		cameraAnim.Play(triggerName.ToString());
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
			virtualCameraElements[i].VirtualCamera.gameObject.SetActive(true);
			_drivenCamera.m_Instructions[i].m_VirtualCamera = virtualCameraElements[i].VirtualCamera;
		}
		
		ChangeState(NameOfAnimationCamera.vCamera_ShowAllMap, StateVirtualCamera.Move);
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

	public ElementCamera GetLookTargetCamera(Transform target, Transform trans) {
		foreach (var e in virtualCameraElements) {
			if (e.IsOnlyLookTarget) {
				e.VirtualCamera.transform.position = trans.position;
				e.VirtualCamera.transform.rotation = trans.rotation;

				e.VirtualCamera.LookAt = target;
				return e;
			}
		}

		return null;
	}

	private float prevValueFOV;
	public void SetFOV(ElementCamera e, float value) {
		prevValueFOV = e.VirtualCamera.m_Lens.FieldOfView;
		e.VirtualCamera.m_Lens.FieldOfView = value;
	}

	public void ResetFOV(ElementCamera e) {
		e.VirtualCamera.m_Lens.FieldOfView = prevValueFOV;
	}
	
	[SerializeField] private Vector3 originRotation;
	[SerializeField] private Vector3 currentnRotation;
	[SerializeField] private Vector3 originPosition;
	private float leftLimit = -40, rightLimit = 40;
	private void Update() {
		if (Input.touchCount == 1 && !isTouchUI) {
			
			Touch t = Input.GetTouch(0);
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
			{
				isTouchUI = true;
				return;
			}

			if (!isTouch) {
				originPosition = t.position;
				currentnRotation = elementCameraPrev.VirtualCamera.transform.rotation.eulerAngles;
				isTouch = true;
				return;
			}

			Vector3 currentRotation = t.position;
			float delta = currentRotation.x - originPosition.x;
			float targetRotateY = currentnRotation.y + delta * 0.1f;
			targetRotateY = Mathf.Clamp(targetRotateY, originRotation.y + leftLimit, originRotation.y + rightLimit);

			Vector3 target = new Vector3(originRotation.x, targetRotateY, originRotation.z);
			
			Quaternion targetQuaternion = Quaternion.Euler(target.x, target.y, target.z);
			elementCameraPrev.VirtualCamera.transform.rotation =
				Quaternion.Slerp(elementCameraPrev.VirtualCamera.transform.rotation, targetQuaternion, 10 * Time.deltaTime);

		}
		else if(Input.touchCount == 0){
			isTouch = false;
			isTouchUI = false;
		}
	}

	private bool isTouch = false, isTouchUI = false;

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