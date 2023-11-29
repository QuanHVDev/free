using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconCatSelected : MonoBehaviour, IPointerUpHandler
{
    public Action OnComplete;
    
    [SerializeField] protected Image iconBG;
    [SerializeField] protected Image imgAvatar;
    [SerializeField] protected TMP_Text txtName;
    protected PeopleSO data;
    protected bool isSelected;
    protected Vector2 offset = new Vector2(0, 100);
    protected IconCatSelection iconCatSelection;
    public PeopleSO Data => data;
    public virtual void SetSelect(bool isSelect)
    {
        if (isSelect) {
            transform.position = Input.GetTouch(0).position + offset;
        }
        else {
            OnComplete?.Invoke();
        }
        
        isSelected = isSelect;
        gameObject.SetActive(isSelected);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        SetSelect(false);
    }

    protected virtual void Update()
    {
        if (Input.touchCount == 0)
        {
            iconCatSelection.SetSelect(false);
            SetSelect(false);
            return;
        }
        
        transform.position = Input.GetTouch(0).position + offset;
    }

    public void Init(PeopleSO data)
    {
        this.data = data;
        imgAvatar.sprite = data.avatar;
        txtName.text = data.name;
        SetSelect(true);
    }

    public void PrevIcon(IconCatSelection iconCatSelection)
    {
        this.iconCatSelection = iconCatSelection;
    }

    public virtual IconCatSelection GetIconSelectionPrev()
    {
        return iconCatSelection;
    }
}
