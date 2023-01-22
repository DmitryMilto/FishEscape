using UnityEngine;
using Sirenix.OdinInspector;

namespace FishEscape.Fishs
{
    [CreateAssetMenu(menuName = "FishEscape/Fishs/Enemy", fileName = "Enemy", order = 1)]
    public class EnemyFish : Fish
    {
        [BoxGroup("Game Data")]
        [VerticalGroup("Game Data/Stats")]
        [LabelWidth(100)]
        [Range(1, 4)]
        [GUIColor(0.8f, 0.4f, 0.4f)]
        public int damage = 1;
    }
}
