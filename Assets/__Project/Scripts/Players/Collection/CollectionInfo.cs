using System;
using UnityEngine;

[Serializable]
public class CollectionInfo
{
    [SerializeField]
    private Sprite realImage;

    [SerializeField]
    [Multiline(4)]
    private string description;
}
