using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ModeManager : MonoBehaviour
{
    protected abstract void Awake();
    public abstract void Init();
}