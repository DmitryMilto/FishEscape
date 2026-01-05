using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ocean_", menuName = "Fish/Ocean", order = 0)]
public class dbOcean : ScriptableObject
{
    public EnumOcean Type;


    public Sprite activeIcon;
    public Sprite deactiveIcon;

    public List<Sprite> sprites;

}
