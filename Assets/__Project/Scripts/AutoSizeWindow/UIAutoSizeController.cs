using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.AutoSize
{
    public class UIAutoSizeController : MonoBehaviour
    {
        [Title("List Windows Auto Size")]
        [SerializeField, PropertyOrder(10), ReadOnly] private List<MonoBehaviour> list;
        protected virtual void Awake()
        {
            InizializeInterfaceIAutoSize();
        }

        protected void InizializeInterfaceIAutoSize()
        {
            var obj = GetComponentsInChildren<MonoBehaviour>();
            foreach (var it in obj)
            {
                if (it is IAutoSize)
                    list.Add(it);
            }
        }
        protected void AutoSizeWindow()
        {
            foreach (var item in list)
            {
                item.GetComponent<IAutoSize>().AutoSize();
            }
        }
    }
}