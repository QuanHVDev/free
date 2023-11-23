using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    [SerializeField] private List<House> houses;

    public void Start()
    {
        foreach (var house in houses)
        {
            house.Init();
        }
    }
}
