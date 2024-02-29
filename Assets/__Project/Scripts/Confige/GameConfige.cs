using FishEscape.Fishs;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Confige", menuName = "FishEscape/Game Confige", order = 1)]
public class GameConfige : ScriptableObject
{
    [SerializeField]
    private EnumOcean ocean;
    public EnumOcean Ocean 
    { 
        get => ocean; 
        set => ocean = value;
    }

    public PlayerFish chooseFish;
    public dbOcean DBOcean;
}
