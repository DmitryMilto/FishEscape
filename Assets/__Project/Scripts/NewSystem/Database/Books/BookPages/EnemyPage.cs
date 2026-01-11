using System;
using FishEscape.Enums.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Books.BookPages
{
    [Serializable]
    public class EnemyPage : APage
    {
        [Header("Game Info")]
        public int Damage;
        public float Speed;
        public override TypeFish TypeFish => TypeFish.Enemy;
    }
}