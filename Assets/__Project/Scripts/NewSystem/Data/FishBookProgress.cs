using System;
using System.Collections.Generic;

namespace __Project.Scripts.NewSystem.Data
{
    [Serializable]
    public class FishBookProgress
    {
        public Dictionary<string, int> CardStates = new(); // string — fishId, int — enum FishCardState
    }
}