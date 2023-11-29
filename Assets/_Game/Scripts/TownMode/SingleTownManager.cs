using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTownManager : MonoBehaviour
{
    [SerializeField] private List<House> houses;
    public void Init()
    {
        for (int i = 0; i < houses.Count; i++)
        {
            houses[i].Init();
            houses[i].SetQuery($"{i}");
        }
    }

    public void Out()
    {
        foreach (var house in houses)
        {
            house.Hide();
        }
    }
    
    public  List<House> Houses => houses;
}
