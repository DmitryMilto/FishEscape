using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ocean_", menuName = "Fish/Ocean", order = 0)]
public class dbOcean : ScriptableObject
{
    [Title("Type Ocean")]
    [EnumToggleButtons]
    public EnumOcean Type;

    [Title("Image")]

    public Sprite activeIcon;
    public Sprite deactiveIcon;

    [Title("Open Fish")]
    public List<Sprite> sprites;

}
