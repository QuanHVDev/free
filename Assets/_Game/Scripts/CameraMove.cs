using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour {
	[SerializeField] private LayerMask mapLayer;
	[SerializeField] private Vector3 OriginPosition;
	[SerializeField] private float leftLimit = -12, rightLimit = 12, botLimit = -10, topLimit = 10;

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

	private void Update() {
		if (Input.touchCount == 1) {
			if (state == StateMouse.inUI) return;
			Touch t = Input.GetTouch(0);
			if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) && state != StateMouse.move) {
				Debug.Log("in UI" + t.phase);
				state = StateMouse.inUI;
				return;
			}

			Debug.Log("in Object" + t.phase);
			if (t.phase == TouchPhase.Began) {
				state = StateMouse.move;
			}

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

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
			0,
			Mathf.Clamp(transform.position.z, botLimit, topLimit)
		);
	}

	public void ResetToOriginPosition() {
		transform.position = OriginPosition;
	}
}