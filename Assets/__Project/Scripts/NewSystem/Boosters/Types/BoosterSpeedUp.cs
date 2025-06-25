using __Project.Scripts.NewSystem.Fishes.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Boosters.Types
{
    public class BoosterSpeedUp : BoosterBase
    {
        public float speedMultiplier = 1.5f;
        public float duration = 5f;
    
        public override void Move()
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
    
        public override void ApplyEffect(PlayerFishBase player)
        {
            player.SetSpeedMultiplier(speedMultiplier, duration);
        }
    }
}