using System;
using __Project.Scripts.NewSystem.Enums;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Elements
{
    [Serializable]
    public class FishBookEntry
    {
        public string fishId;
        public string fishName;
        public FishType fishType;
        public Sprite oceanBackground;
        public Sprite gameFishSprite;
        public Sprite realFishSprite;
        [TextArea] public string description;
        public TypeOceans ocean;
        public FishCardInitialState initialState = FishCardInitialState.Locked;
    }
    public enum FishCardInitialState
    {
        Locked,
        Open
    }
}