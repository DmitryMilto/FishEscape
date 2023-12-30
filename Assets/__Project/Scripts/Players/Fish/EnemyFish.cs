using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using FishEscape.Enums;

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

        [BoxGroup("Other Setting")]
        [Range(0f,1f)]
        public float scale = .1f;

        [BoxGroup("Other Setting")]
        public List<ListBuffer> list;
        [BoxGroup("Other Setting")]
        public EnumTypeMove typeMove;
    }
}
