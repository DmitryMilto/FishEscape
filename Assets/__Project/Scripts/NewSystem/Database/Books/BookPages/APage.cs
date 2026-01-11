using FishEscape.Enums.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Books.BookPages
{
    public abstract class APage
    {
        [Header("Base Info")] 
        public string Name;
        public string Description;

        [Header("Type Info")] 
        public abstract TypeFish TypeFish { get; }

        [Header("Visual Info")] 
        public Sprite GameIcon;
        // public Sprite ReaLIcon;
    }
}