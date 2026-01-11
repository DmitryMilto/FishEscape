using System.Collections.Generic;
using __Project.Scripts.NewSystem.Data.Books;
using __Project.Scripts.NewSystem.Database.Books.BookPages;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Books
{
    [CreateAssetMenu(fileName = "BookEnemies", menuName = "Database/Books/BookEnemies", order = 2)]
    public class dbBookEnemies: ScriptableObject
    {
        [SerializeField] private List<EnemyPage> _enemyPages;
        
        public List<CardFishIcon> GetAllCardFish()
        {
            var cards = new List<CardFishIcon>(_enemyPages.Count);
            foreach (var playerPage in _enemyPages)
            {
                var card = new CardFishIcon
                {
                    FishId = playerPage.Name,
                    Icon = playerPage.GameIcon
                };
                cards.Add(card);
            }
            return cards;
        }
        public EnemyPage GetCardFishById(string cardFishId) => _enemyPages.FindLast(x => x.Name == cardFishId);
    }
}