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
    
    [SerializeField] private Image iconBG;
    [SerializeField] private Image imgAvatar;
    [SerializeField] private TMP_Text txtName;
    private PeopleSO data;
    private bool isSelected;
    private Vector2 offset = new Vector2(0, 100);
    private IconCatSelection iconCatSelection;
    public PeopleSO Data => data;
    public void SetSelect(bool isSelect)
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

    public void OnPointerUp(PointerEventData eventData)
    {
        SetSelect(false);
    }

    private void Update()
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

    public IconCatSelection GetIconSelectionPrev()
    {
        return iconCatSelection;
    }
}
