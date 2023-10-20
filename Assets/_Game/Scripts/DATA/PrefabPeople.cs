using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPeople : MonoBehaviour {
	public const string DOG_EAT = "eat";
	public const string DOG_JUMP_PLAY = "jump_play";
	public const string DOG_PAXIA = "paxia";
	public const string DOG_SHUMAO = "shumao";
	public const string DOG_SIT = "sit";
	public const string DOG_SIT_ZHAOJINGZI = "sit_zhaojingzi";
	public const string DOG_XIZAO = "xizao";

	[SerializeField] private Animator animator;

	public void ChangeState(string newState) {
		animator.Play(newState);
	}

	[Serializable]
	public enum stateDog {
		eat,
		jump_play,
		paxia,
		shumao,
		sit,
		sit_zhaojingzi,
		xizao
	}

	public stateDog state;

	[ContextMenu("PlayAnim")]
	public void PlayAnim() {
		ChangeState(state.ToString());
	}
	
	[ContextMenu("PlayAnimE")]
	public void PlayAnimE() {
		ChangeState(DOG_EAT);
	}
}