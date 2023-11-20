using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WaitCondition : Condition
{
    protected override void DoneCorrect()
    {
        EnableObjectsToHide(false);
        EnableObjectsToShow(true);
    }
}
