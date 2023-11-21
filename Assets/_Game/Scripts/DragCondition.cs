using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class DragCondition : Condition
{
    [SerializeField] protected Condition targetCondition;
    [SerializeField] private Transform correctedTrans;
    [SerializeField] private float distanceToTarget = 4f;

    protected override void OnMouseDrag()
    {
        if (IsCanShow) return;
        transform.position = GetMousePosition();
        Debug.Log("Drag " + gameObject.name);
    }

    protected override void OnMouseUp()
    {
        if (!IsCheckPrevCondition()) return;
        if (Vector3.Distance(targetCondition.transform.position, transform.position) < distanceToTarget)
        {
            IsCanShow = true;
            transform.parent = correctedTrans;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
    }

    protected override void OnMouseDown()
    {
        
    }
}