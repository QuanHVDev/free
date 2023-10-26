using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class PrefabPeople : MonoBehaviour {
	public const string IDLE = "Armature|Cat_01_Idle";
	public const string HAPPY = "Armature|Cat_02_Happy";
	public const string RUN = "Armature|Cat_03_Run";

	[SerializeField] private Animator animator;

	private void ChangeState(string newState) {
		animator.Play(newState);
	}

	public void StartRandomAnimFinishTarget() {
		coroutine = StartCoroutine(StartRandomAnimFinishTargetSync());
	}

	private Coroutine coroutine;

	private IEnumerator StartRandomAnimFinishTargetSync() {
		float nextTimePlayAnim = Time.time + Random.Range(0, 5f);
		yield return new WaitWhile(() => {
			if (nextTimePlayAnim > Time.time) {
				string anim = Random.Range(0, 2) == 1 ? IDLE : HAPPY;
				ChangeState(anim);
			}

			return true;
		});
	}

	public void DoneLevel() {
		StopCoroutine(coroutine);
		
	}
	

	public void SetTargetToMove(Transform trans) {
		if (TryGetComponent(out NavMeshAgent nav)) {
			nav.SetDestination(trans.position);
			ChangeState(RUN);
		}
	}
}