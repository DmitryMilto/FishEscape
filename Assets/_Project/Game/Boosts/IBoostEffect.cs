using _Project.Game.Player;

namespace _Project.Game.Boosts
{
    public interface IBoostEffect
    {
        void Apply(FishStats stats);
    }
}