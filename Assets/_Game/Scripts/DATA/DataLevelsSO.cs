using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GAME/Levels", fileName = "LevelsSO")]
public class DataLevelsSO : ScriptableObject {
    public List<MapManager> MapManagers;
}
