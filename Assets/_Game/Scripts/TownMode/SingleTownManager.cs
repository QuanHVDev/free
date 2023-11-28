using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleTownManager : MonoBehaviour
{
    [SerializeField] private List<House> houses;
    public void Init()
    {
        foreach (var house in houses)
        {
            house.Init();
        }
    }

    public void Out()
    {
        foreach (var house in houses)
        {
            house.Hide();
        }
    }
}
