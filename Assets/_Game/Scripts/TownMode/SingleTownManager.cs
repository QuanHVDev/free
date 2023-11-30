using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SingleTownManager : MonoBehaviour
{
    [SerializeField] private List<House> houses;

    public void Out()
    {
        foreach (var house in houses)
        {
            house.EnableNoti(false);
        }
    }
    
    public List<House> GetHouses() => houses;
}
