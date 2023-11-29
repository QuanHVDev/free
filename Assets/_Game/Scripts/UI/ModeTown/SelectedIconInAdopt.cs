using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IconCatSelectedInAdopt : IconCatSelected
{
    [SerializeField] private List<GameObject> objs;
    public void ActiveAllObject(bool enable)
    {
        foreach (var obj in objs)
        {
            obj.SetActive(enable);
        }
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
