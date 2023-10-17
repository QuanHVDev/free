using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {
	[SerializeField] private LayerMask mapLayer;
	private Vector2 firstPosition;

	private void Start() {
		Input.multiTouchEnabled = false;
	}

	private void FixedUpdate() {
		if (Input.touchCount > 0) {
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)){
				return;
			}
			
			Touch t = Input.GetTouch(0);
			Ray ray = Camera.main.ScreenPointToRay(t.position);
			if (Physics.Raycast(ray, out RaycastHit hit, 999f, mapLayer) && firstPosition == Vector2.zero) {
				firstPosition = t.position;
				return;
			}

			MoveCameraWith(t.position - firstPosition);
			firstPosition = t.position;
		}
		else {
			firstPosition = Vector2.zero;
		}
	}

	private void MoveCameraWith(Vector2 deltaPosition) {
		transform.position -= new Vector3(deltaPosition.x, 0, deltaPosition.y) * Time.deltaTime;
		//Debug.Log(tPosition);
	}
}