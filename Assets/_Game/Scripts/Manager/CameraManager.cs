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
	[SerializeField] private Vector3 currentRotation;
	[SerializeField] private Vector3 originPosition;
	[SerializeField] private Vector3 currentPosition;
	private float leftLimit = -40, rightLimit = 40;
	private void Update() {
		if (Input.touchCount == 1) {
			
			Touch t = Input.GetTouch(0);

			if (ModeFindCatManager.Instance.TouchUIState == ModeFindCatManager.StateTouch.free)
			{
				if (!isTouch) {
					this.originPosition = t.position;
					this.currentRotation = elementCameraPrev.VirtualCamera.transform.rotation.eulerAngles;
					isTouch = true;
					return;
				}

				if (Vector3.Distance(originPosition,t.position) > Mathf.Epsilon) {
					ModeFindCatManager.Instance.TouchUIState = ModeFindCatManager.StateTouch.touchRotate;
					Debug.Log("Start");
				}
			}

			if (ModeFindCatManager.Instance.TouchUIState != ModeFindCatManager.StateTouch.touchRotate) return;
			
			currentPosition = t.position;
			delta = currentPosition.x - originPosition.x;
			targetRotateY = currentRotation.y - delta * 0.04f;
			targetRotateY = Mathf.Clamp(targetRotateY, originRotation.y + leftLimit, originRotation.y + rightLimit);
			target = new Vector3(originRotation.x, targetRotateY, originRotation.z);
			
			Quaternion targetQuaternion = Quaternion.Euler(target.x, target.y, target.z);
			elementCameraPrev.VirtualCamera.transform.rotation =
				Quaternion.Slerp(elementCameraPrev.VirtualCamera.transform.rotation, targetQuaternion, 10 * Time.deltaTime);
			Debug.Log("running");
		}
		else if(Input.touchCount == 0){
			isTouch = false;
			ModeFindCatManager.Instance.TouchUIState = ModeFindCatManager.StateTouch.free;
		}
	}

	private Vector3 target;
	private float delta, targetRotateY;
	private bool isTouch = false;
}