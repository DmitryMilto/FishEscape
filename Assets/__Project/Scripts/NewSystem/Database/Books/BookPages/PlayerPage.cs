using System;
using FishEscape.Enums.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Books.BookPages
{
    [Serializable]
    public class PlayerPage : APage
    {
        [Header("Game Info")] 
        public int Health;
        public float Speed;
        public override TypeFish TypeFish => TypeFish.Player;
    }
}