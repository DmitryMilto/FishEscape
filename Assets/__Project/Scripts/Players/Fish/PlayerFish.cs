using Sirenix.OdinInspector;
using UnityEngine;

namespace FishEscape.Fishs
{
    [CreateAssetMenu(menuName = "FishEscape/Fishs/Player", fileName = "Player", order = 1)]
    public class PlayerFish : Fish
    {
        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(1, 5)]
        [GUIColor(0.5f, 1f, 0.5f)]
        public int health = 1;
    }
}
