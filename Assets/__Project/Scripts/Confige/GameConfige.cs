using FishEscape.Fishs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Confige", menuName = "FishEscape/Game Confige", order = 1)]
public class GameConfige : ScriptableObject
{
    public PlayerFish chooseFish;
    public List<EnemyFish> chooseEnemy;
}
