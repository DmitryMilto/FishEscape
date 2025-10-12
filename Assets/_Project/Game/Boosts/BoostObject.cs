using _Project.Core.Utils;
using _Project.Data.Boosts;
using _Project.Game.Player;
using UnityEngine;

namespace _Project.Game.Boosts
{
    [RequireComponent(typeof(Collider2D))]
    public class BoostObject : MonoBehaviour
    {
        private BoostData _data;
        private FishStats _targetStats;
        private bool _collected;

        public void Init(BoostData data, FishStats targetStats)
        {
            _data = data;
            _targetStats = targetStats;
            _collected = false;

            TDebug.Log($"[BoostObject] Initialized with boost: {_data.name}");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_collected || _data == null || _targetStats == null)
                return;

            if (!other.CompareTag("Player")) return;

            var effect = _data.GetEffect();
            effect?.Apply(_targetStats);

            _collected = true;
            Destroy(gameObject);
        }
    }
}