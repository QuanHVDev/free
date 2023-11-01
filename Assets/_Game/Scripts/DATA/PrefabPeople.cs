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
		StartCoroutine(StartRandomAnimFinishTargetSync());
	}

	private IEnumerator StartRandomAnimFinishTargetSync() {
		ChangeState(HAPPY);
		var x = animator.GetCurrentAnimatorClipInfo(0);
		if(x != null && x.Length > 0) yield return new WaitForSeconds(x[0].clip.length);
		
		float nextTimePlayAnim = Time.time - 0.01f;
		float timeEndAnim = Time.time + 15f;
		yield return new WaitWhile(() => {
			
			if (nextTimePlayAnim < Time.time) {
				nextTimePlayAnim = Time.time + Random.Range(3, 7f);
				string anim = Random.Range(0, 2) == 1 ? IDLE : HAPPY;
				ChangeState(anim);
			}

			if (timeEndAnim < Time.time) {
				ChangeState(IDLE);
				return false;
			}

			return true;
		});
	}

	public void SetTargetToMove(Transform trans) {
		if (TryGetComponent(out NavMeshAgent nav)) {
			nav.SetDestination(trans.position);
			ChangeState(RUN);
		}
	}
}