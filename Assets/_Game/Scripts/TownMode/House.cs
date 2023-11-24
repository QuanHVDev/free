using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class House : MonoBehaviour
{
    private const string STRING_NAMEANIM_NOTI = "IsVisible";
    
    [SerializeField] private List<TagCat> TagCats;
    [SerializeField] private Animator notiAnimator;

    public void Init()
    {
        EnableNoti(true);
    }

    private async void EnableNoti(bool enable)
    {
        if (enable) notiAnimator.transform.parent.gameObject.SetActive(enable);
        if (TagCats != null && TagCats.Count > 0)
        {
            notiAnimator.SetBool(STRING_NAMEANIM_NOTI, enable);
        }

        await Task.Delay((int)(notiAnimator.runtimeAnimatorController.animationClips[0].length * 1000));
        if (!enable) notiAnimator.transform.parent.gameObject.SetActive(enable);
    }
    
    public void Interact()
    {
        Debug.Log($"{gameObject.name} interact!");
    }

    public void Hide()
    {
        EnableNoti(false);
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
