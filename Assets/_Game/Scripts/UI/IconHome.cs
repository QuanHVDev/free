using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconHome : MonoBehaviour {
	public Transform homeModel { get; private set; }

	public void SetHomeModel(Transform homeModel) {
		this.homeModel = homeModel;
	}
}
