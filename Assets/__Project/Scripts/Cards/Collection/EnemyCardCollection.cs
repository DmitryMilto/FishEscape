using FishEscape.Fishs;

namespace Scripts.Card.Collection
{
    public class EnemyCardCollection : BaseCardCollection<EnemyFish>
    {
        public override void SetCard(EnemyFish fish)
        {
            this.Card = fish;
            InitCard();
        }
    }
}
