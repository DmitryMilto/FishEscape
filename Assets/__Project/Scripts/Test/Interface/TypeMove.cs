using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TypeMove
{
    protected float speed;
    protected TypeMove(float speed)
    {
        this.speed = speed;
    }
}
