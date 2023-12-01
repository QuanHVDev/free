using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AdoptUI : BaseUIElement
{
    [SerializeField] private RectTransform mainCanvas;
    
    [SerializeField] private IconAdopt iconRequest;
    [SerializeField] private Transform contentRequest;
    [SerializeField] private IconCatSelectedInAdopt selectedIconSelectedInAdopt;
    [SerializeField] private Transform contentSelectedInAdopt;
    [SerializeField] private RectTransform boxRectTrans;
    
    private readonly float leftAnchor = 100f, midAnchor = 0, rightAnchor = -100f;
    private List<IconAdopt> requestIcons;
    private List<IconCatSelectedInAdopt> selectedIcons;
    public List<IconAdopt> RequestIcons => requestIcons;
    private Vector2 sizeMainCanvas;
    public override void OnAwake()
    {
        
    }

    private void Start()
    {
        iconRequest.gameObject.SetActive(false);
        selectedIconSelectedInAdopt.gameObject.SetActive(false);
        sizeMainCanvas = new Vector2(mainCanvas.rect.width * mainCanvas.localScale.x,
            mainCanvas.rect.height * mainCanvas.localScale.y);
    }

    public void ShowRequest(List<TagCat> tags, House houseTransform)
    {
        ResetAllIcon();
        ResetFilledAdopt();
        foreach (var tag in tags)
        {
            // Spawn Request
            var obj = GetIcon();
            obj.Init(tag);
            
            // Spawn Selected
            var iconSelected = GetSelectedIconInAdopt();
            iconSelected.ActiveAllObject(false);
            iconSelected.gameObject.SetActive(true);
        }

        var targetPosition = Camera.main.WorldToScreenPoint(houseTransform.transform.position);
        float targetAnchor = midAnchor;
        
        // ở bên trai màn hình và quá 1 nửa width của box
        if (targetPosition.x < boxRectTrans.rect.width/2 && sizeMainCanvas.x/2 > targetPosition.x)
        {
            targetAnchor = leftAnchor;
        }
        // ở bên phaải màn hình và quá 1 nửa width của box
        else if (sizeMainCanvas.x - targetPosition.x < boxRectTrans.rect.width/2 && sizeMainCanvas.x/2 < targetPosition.x)
        {
            targetAnchor = rightAnchor;
        }
        
        mainFrame.transform.position = targetPosition;
        boxRectTrans.localPosition = new Vector3(targetAnchor, boxRectTrans.localPosition.y, boxRectTrans.localPosition.z);
    }

    private void ResetAllIcon()
    {
        if (requestIcons == null)
        {
            requestIcons = new List<IconAdopt>();
        }
        else
        {
            foreach (var icon in requestIcons)
            {
                icon.Reset();
            }
        }
    }

    private IconAdopt GetIcon()
    {
        if (requestIcons == null)
        {
            requestIcons = new List<IconAdopt>();
        }
        
        foreach (var icon in requestIcons)
        {
            if (icon.Tag == TagCat.None)
            {
                icon.gameObject.SetActive(true);
                return icon;
            }
        }

        var obj = Instantiate(iconRequest, contentRequest);
        obj.gameObject.SetActive(true);
        requestIcons.Add(obj);
        return obj;
    }

    public bool CheckCatWithTags(List<TagCat> tags, IconAdopt icon)
    {
        if (icon.Tag == TagCat.Any) return true;
        foreach (var tag in tags) {
            if (requestIcons.Contains(icon) && tag == icon.Tag)
                return true;
        }
        
        return false;
    }

    public void FilledAdopt(int indexTag, PeopleSO data, bool isCanUndo)
    {
        if (indexTag >= selectedIcons.Count || !selectedIcons[indexTag].gameObject.activeSelf)
        {
            Debug.Log($"NULL With IndexTag {indexTag}");
            return;
        }
        
        selectedIcons[indexTag].Init(data);
        selectedIcons[indexTag].ActiveAllObject(true);
        requestIcons[indexTag].ActiveTagText(false);
        if(isCanUndo) selectedIcons[indexTag].ActiveUndoButton(indexTag);
    }

    private IconCatSelectedInAdopt GetSelectedIconInAdopt()
    {
        if (selectedIcons == null) {
            selectedIcons = new List<IconCatSelectedInAdopt>();
        }

        foreach (var selected in selectedIcons)
        {
            if (!selected.gameObject.activeSelf)
            {
                return selected;
            }
        }

        var icon = Instantiate(selectedIconSelectedInAdopt, contentSelectedInAdopt);
        icon.gameObject.SetActive(true);
        selectedIcons.Add(icon);
        return icon;
    }

    public void ResetFilledAdopt()
    {
        if (selectedIcons == null) return;
        foreach (var icon in selectedIcons)
        {
            icon.ActiveAllObject(true);
            icon.gameObject.SetActive(false);
        }
    }
}
