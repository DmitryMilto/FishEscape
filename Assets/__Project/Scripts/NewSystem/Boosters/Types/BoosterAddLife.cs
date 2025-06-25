using __Project.Scripts.NewSystem.Fishes.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Boosters.Types
{
    public class BoosterAddLife : BoosterBase
    {
        public override void Move()
        {
            transform.Translate(Vector3.left * (speed * Time.deltaTime));
        }
    
        public override void ApplyEffect(PlayerFishBase player)
        {
            player.AddLife(1);
        }
    }
}