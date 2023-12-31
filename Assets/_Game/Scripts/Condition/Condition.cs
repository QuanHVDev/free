using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Condition : MonoBehaviour
{
    public bool IsCanShow { get; protected set; } = false;

    [SerializeField] protected List<GameObject> objsToHide;
    [SerializeField] protected List<GameObject> objsToShow;
    [SerializeField] protected List<Condition> prevConditionList;
    [SerializeField] protected List<Animator> animators;
    [SerializeField] protected LayerMask mouseColliderLayerMark;

    protected bool IsCheckPrevCondition()
    {
        if (prevConditionList == null) return true;
        foreach (var condition in prevConditionList)
        {
            if (!condition.IsCanShow)
            {
                return false;
            }
        }

        return true;
    }


    protected Vector3 GetMousePosition()
    {
        var mouseScreenPos = Input.mousePosition;
        Ray ray = UnityEngine.Camera.main.ScreenPointToRay(mouseScreenPos);
        if (Physics.Raycast(ray, out RaycastHit hit, 999f, mouseColliderLayerMark))
        {
            Debug.DrawLine(ray.origin, hit.point, Color.black);
            return hit.point;
        }
        else
        {
            mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
            Debug.DrawRay(ray.origin, ray.direction * 30, Color.red);
        }

        return UnityEngine.Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }

    protected virtual void OnMouseDown()
    {
    }

    protected virtual void OnMouseDrag()
    {
        if (!IsCheckPrevCondition()) return;
    }

    protected virtual void OnMouseUp()
    {
    }

    protected void EnableObjectsToHide(bool isEnable)
    {
        if (objsToHide != null)
        {
            foreach (var o in objsToHide)
            {
                o.SetActive(isEnable);
            }
        }
    }

    protected void EnableObjectsToShow(bool isEnable)
    {
        if (objsToShow != null)
        {
            foreach (var o in objsToShow)
            {
                o.SetActive(isEnable);
            }
        }
    }
    
    protected IEnumerator EnableAnimationsAsync(bool isPlay)
    {
        if (animators != null && animators.Count > 0)
        {
            foreach (var a in animators)
            {
                a.SetBool("condition", isPlay);
                yield return new WaitForSeconds(a.runtimeAnimatorController.animationClips[0].length);
            }
        }

        yield return null;
    }
    
    protected IEnumerator DoneCorrectAsync()
    {
        EnableObjectsToHide(false);
        EnableObjectsToShow(true);
        yield return EnableAnimationsAsync(true);
        IsCanShow = true;
    }
}