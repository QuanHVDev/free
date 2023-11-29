using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GAME/Cats", fileName = "CatsSO")]
public class CatsDataSO : ScriptableObject
{
    public List<PeopleSO> ListData;
}
