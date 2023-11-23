using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private const string STRING_NAMEANIM_NOTI = "IsVisible";
    
    [SerializeField] private List<TagCat> TagCats;
    [SerializeField] private Animator notiAnimator;

    public void Init()
    {
        if (TagCats != null && TagCats.Count > 0)
        {
            notiAnimator.SetBool(STRING_NAMEANIM_NOTI, true);
        }
    }

    public void Interact()
    {
        Debug.Log($"{gameObject.name} interact!");
    }
}


[Serializable]
public enum TagCat
{
    Any,
    Male,
    Female,
    Doctor,
    Farmer,
}
