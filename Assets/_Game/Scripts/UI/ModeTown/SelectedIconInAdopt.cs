using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IconCatSelectedInAdopt : IconCatSelected
{
    [SerializeField] private Button btnUndo;
    [SerializeField] private List<GameObject> objs;
    private int indexTag = -1;
    
    private void Start()
    {
        btnUndo.onClick.AddListener(()=>
        {
            ModeTownManager.Instance.UndoCatSelected(indexTag, data);
        });
    }

    public void ActiveAllObject(bool enable)
    {
        foreach (var obj in objs)
        {
            obj.SetActive(enable);
        }
        
        if(!enable) btnUndo.gameObject.SetActive(false);
    }

    public void ActiveUndoButton(int index)
    {
        this.indexTag = index;
        btnUndo.gameObject.SetActive(true);
    }

    protected override void Update()
    {
        
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public override void SetSelect(bool isSelect)
    {
        
    }
}
