using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AdoptUI : BaseUIElement
{
    [SerializeField] private IconAdopt iconRequest;
    [SerializeField] private Transform contentRequest;
    [SerializeField] private RectTransform boxRectTrans;
    
    private readonly float leftAnchor = 100f, midAnchor = 0, rightAnchor = -100f;
    private List<IconAdopt> icons;
    
    public override void OnAwake()
    {
        
    }

    private void Start()
    {
        iconRequest.gameObject.SetActive(false);
    }

    public void ShowRequest(List<TagCat> tags, House houseTransform)
    {
        ResetAllIcon();
        foreach (var tag in tags)
        {
            var obj = GetIcon();
            obj.Init(tag);
        }

        var targetPosition = Camera.main.WorldToScreenPoint(houseTransform.transform.position);
        float targetAnchor = midAnchor;
        if (targetPosition.x < 450)
        {
            targetAnchor = leftAnchor;
        }
        else if (targetPosition.x > 960)
        {
            targetAnchor = rightAnchor;
        }
        
        mainFrame.transform.position = targetPosition;
        boxRectTrans.localPosition = new Vector3(targetAnchor, boxRectTrans.localPosition.y, boxRectTrans.localPosition.z);

    }

    private void ResetAllIcon()
    {
        if (icons == null)
        {
            icons = new List<IconAdopt>();
        }
        else
        {
            foreach (var icon in icons)
            {
                icon.Reset();
            }
        }
    }

    private IconAdopt GetIcon()
    {
        if (icons == null)
        {
            icons = new List<IconAdopt>();
        }
        
        foreach (var icon in icons)
        {
            if (icon.Tag == TagCat.None)
            {
                icon.gameObject.SetActive(true);
                return icon;
            }
        }

        var obj = Instantiate(iconRequest, contentRequest);
        obj.gameObject.SetActive(true);
        icons.Add(obj);
        return obj;
    }
    
}
