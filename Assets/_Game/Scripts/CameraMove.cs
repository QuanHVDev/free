using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {
	[SerializeField] private LayerMask mapLayer;
	[SerializeField] private Vector3 OriginPosition;
	private Vector2 firstPosition;

	private enum StateMouse {
		wait,
		move,
		inUI
	}

	private StateMouse state;

	private void Start() {
		Input.multiTouchEnabled = false;
		state = StateMouse.wait;
	}

	private void FixedUpdate() {
		if (Input.touchCount > 0 ) {
			if (state == StateMouse.inUI) return;
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) {
				state = StateMouse.inUI;
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
			state = StateMouse.wait;
		}
	}

	private void MoveCameraWith(Vector2 deltaPosition) {
		transform.position -= new Vector3(deltaPosition.x, 0, deltaPosition.y) * Time.deltaTime;
		state = StateMouse.move;
	}

	public void ResetToOriginPosition() {
		transform.position = OriginPosition;
	}
}