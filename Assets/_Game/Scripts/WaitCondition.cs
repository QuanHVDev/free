using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaitCondition : Condition
{
    private void Start()
    {
        StartCoroutine(CheckingPrevCondition());
    }

    private IEnumerator CheckingPrevCondition()
    {
        yield return new WaitUntil(() =>
        {
            return IsCheckPrevCondition();
        });

        yield return DoneCorrectAsync();
    }
}
