using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    public bool IsCanShow { get; protected set; } = false;
    public Collider colliderTrigger;

    public List<GameObject> objsToHide;
    public List<GameObject> objsToShow;

    public virtual void ClickObject()
    {
        if (objsToHide != null)
        {
            foreach (var o in objsToHide)
            {
                o.SetActive(false);
            }
        }

        if (objsToShow != null)
        {
            foreach (var o in objsToShow)
            {
                o.SetActive(true);
            }
        }
    }
}