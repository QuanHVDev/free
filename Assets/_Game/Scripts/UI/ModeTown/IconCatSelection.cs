using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconCatSelection : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Image imgAvatar;
    [SerializeField] private TMP_Text txtName;
    private PeopleSO data;
    private bool isSelected;
    private Vector2 offset = new Vector2(0, 100);
    public PeopleSO Data => data;

    public void Init(PeopleSO data)
    {
        this.data = data;
        imgAvatar.sprite = data.avatar;
        txtName.text = data.name;
        SetSelect(false);
    }

    public void SetSelect(bool isSelect)
    {
        isSelected = isSelect;
        gameObject.SetActive(!isSelected);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        SetSelect(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        var icon = ModeTownManager.Instance.SetSelection(data);
        icon.PrevIcon(this);
        SetSelect(true);
    }

    public void Reset()
    {
        this.data = null;
        gameObject.SetActive(false);
    }
}
