using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class DotweenRunAnim : MonoBehaviour
{
    private float timRunAnim = 0f;
    public void RunAnim()
    {
        timRunAnim = Time.time;
    }

    private void Update()
    {
        if (timRunAnim == -1 || timRunAnim > Time.time)
        {
            return;
        }

        transform.DOPunchScale(Vector3.one * 1.01f, 0.3f, 4, 0.1f).SetLoops(2, LoopType.Yoyo);
        timRunAnim += 3f;
    }

    public void StopAnim()
    {
        timRunAnim = -1;
    }
}
