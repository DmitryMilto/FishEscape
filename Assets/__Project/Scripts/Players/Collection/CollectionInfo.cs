using Sirenix.OdinInspector;
using System;
using UnityEngine;

[Serializable]
public class CollectionInfo
{
    [SerializeField]
    [PreviewField(45)]
    private Sprite realImage;

    [SerializeField]
    [Multiline(4)]
    private string description;
}
