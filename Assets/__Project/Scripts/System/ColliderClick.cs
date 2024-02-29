using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderClick : MonoBehaviour
{
    [SerializeField]
    private EnumPoint point;
    [SerializeField]
    private TargetMap targetMap;

    private TargetMap Target
    {
        get
        {
            if(targetMap == null)
                targetMap = GetComponentInParent<TargetMap>();
            return targetMap;
        }
    }

    private void OnMouseDown()
    {
        Target.SwitchTarget(point);
    }
}
