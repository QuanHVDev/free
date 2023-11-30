using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SingleTownManager : MonoBehaviour
{
    [SerializeField] private List<House> houses;
    public void Init()
    {
        for (int i = 0; i < houses.Count; i++)
        {
            houses[i].SetQuery($"{i}");
        }
    }

    public void Out()
    {
        foreach (var house in houses)
        {
            house.EnableNoti(false);
        }
    }
    
    public  List<House> GetHouses() => houses;
}
