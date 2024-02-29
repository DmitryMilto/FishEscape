using FishEscape.Fishs;

namespace Scripts.Card.Collection
{
    public class PlayerCardCollection : BaseCardCollection<PlayerFish>
    {
        public override void SetCard(PlayerFish fish)
        {
            this.Card = fish;
            InitCard();
        }
    }
}
