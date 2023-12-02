using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class House : MonoBehaviour
{
    private const string STRING_NAMEANIM_NOTI = "IsVisible";
    private string query = "";
    [SerializeField] private List<TagCat> TagCats;
    [SerializeField] private Animator notiAnimator;

    public async void EnableNoti(bool enable)
    {
        if (enable) notiAnimator.transform.parent.gameObject.SetActive(enable);
        if (TagCats != null && TagCats.Count > 0)
        {
            notiAnimator.SetBool(STRING_NAMEANIM_NOTI, enable);
        }

        await Task.Delay((int)(notiAnimator.runtimeAnimatorController.animationClips[0].length * 1000));
        if (!enable && notiAnimator) notiAnimator.transform.parent.gameObject.SetActive(enable);
    }
    
    public void Interact()
    {
        ModeTownManager.Instance.ShowRequestHouse(TagCats, this);
    }

    public int CountTagCats() => TagCats.Count;
}


[Serializable]
public enum TagCat
{
    Any,
    Male,
    Female,
    Doctor,
    Farmer,
    None,
}
