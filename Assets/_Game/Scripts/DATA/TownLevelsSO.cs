using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GAME/Towns", fileName = "TownsSO")]
public class TownLevelsSO : ScriptableObject
{
    public List<TownLevel> Data;
}
[Serializable]
public class TownLevel
{
    public string nameTown;
    public SingleTownManager SingleTownManager;
}
