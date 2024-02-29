using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscoveryFish : MonoBehaviour
{
    [SerializeField]
    private Sprite iconCloseFish;
    [SerializeField]
    private List<SpriteRenderer> renderers;

    private void OnValidate()
    {
        if(renderers.Count == 0)
            renderers = GetComponentsInChildren<SpriteRenderer>().ToList();
    }
}
