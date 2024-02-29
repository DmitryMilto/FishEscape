using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Scripts.Save
{
    [Serializable]
    public class PlayerSaveData : BaseFishSaveData
    {
        [Title("Puzzle")]
        public int TargetPuzzle;
    }
}
