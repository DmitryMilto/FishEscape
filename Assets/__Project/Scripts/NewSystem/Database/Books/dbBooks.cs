using System.Collections.Generic;
using __Project.Scripts.NewSystem.Data.Books;
using __Project.Scripts.NewSystem.Database.Books.BookPages;
using FishEscape.Enums.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Books
{
    [CreateAssetMenu(fileName = "Books", menuName = "Database/Books/AllBooks", order = 1)]
    public class dbBooks : ScriptableObject
    {
        [SerializeField] private dbBookPlayers bookPlayers;
        [SerializeField] private dbBookEnemies bookEnemies;

        public APage GetCardFishById(TypeFish typeFish, string fishId)
        {
            return typeFish switch
            {
                TypeFish.Player => bookPlayers.GetCardFishById(fishId),
                TypeFish.Enemy => bookEnemies.GetCardFishById(fishId),
                _ => null
            };
        }
        public List<CardFishIcon> GetCardFishByType(TypeFish typeFish)
        {
            return typeFish switch
            {
                TypeFish.Player => bookPlayers.GetAllCardFish(),
                TypeFish.Enemy => bookEnemies.GetAllCardFish(),
                _ => new List<CardFishIcon>()
            };
        }
    }
}