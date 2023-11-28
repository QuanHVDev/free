using System;
using UnityEngine;

[CreateAssetMenu(menuName = "GAME/People", fileName = "PeopleSO")]
[Serializable]
public class PeopleSO : ScriptableObject
{
    public string id;
    public Sprite avatar;
    public string name;
    public string speciality;
    public PrefabPeople prefab;
    public ParticleSystem vfxHide;
}
