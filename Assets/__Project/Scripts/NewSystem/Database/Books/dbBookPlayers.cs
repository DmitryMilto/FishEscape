using System.Collections.Generic;
using __Project.Scripts.NewSystem.Data.Books;
using __Project.Scripts.NewSystem.Database.Books.BookPages;
using FishEscape.Enums.Players;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Database.Books
{
    [CreateAssetMenu(fileName = "BookPlayers", menuName = "Database/Books/BookPlayers", order = 2)]
    public class dbBookPlayers : ScriptableObject
    {
        [SerializeField] private List<PlayerPage> _playerPages;

        public List<CardFishIcon> GetAllCardFish()
        {
            var cards = new List<CardFishIcon>(_playerPages.Count);
            foreach (var playerPage in _playerPages)
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
        public PlayerPage GetCardFishById(string cardFishId) => _playerPages.FindLast(x => x.Name == cardFishId);
    }
}