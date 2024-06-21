using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerBase<T> : MonoBehaviour where T : Enum
{
    public InitializeBase<T> initialize { get; protected set; }

    protected virtual void Start()
    {
        initialize = GetComponent<InitializeBase<T>>();
    }
    protected abstract void OnTriggerEnter2D(Collider2D collision);
}
