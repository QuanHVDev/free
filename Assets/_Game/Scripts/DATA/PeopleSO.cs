using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GAME/People", fileName = "PeopleSO")]
public class PeopleSO : ScriptableObject {
    public Sprite avatar;
    public string name;
    public string speciality;
    public PrefabPeople prefab;
    public ParticleSystem vfxHide;
}
