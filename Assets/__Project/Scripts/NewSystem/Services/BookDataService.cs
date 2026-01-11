using System.Collections.Generic;
using System.Linq;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Data.Books;
using __Project.Scripts.NewSystem.Database.Books;
using __Project.Scripts.NewSystem.Database.Books.BookPages;
using __Project.Scripts.NewSystem.Enums.Audios;
using __Project.Scripts.NewSystem.Tools;
using __Project.Scripts.NewSystem.Views.Home;
using __Project.Scripts.NewSystem.Views.Managers;
using __Project.Scripts.NewSystem.Elements.UI.Books;
using FishEscape.Enums.Players;
using JetBrains.Annotations;

namespace __Project.Scripts.NewSystem.Services
{
    [UsedImplicitly]
    public class BookDataService
    {
        private readonly dbBooks _book;
        private readonly ViewManager _viewManager;

        private int _currentPage = 1;
        private TypeFish _currentTypeFish = TypeFish.Player;

        public BookDataService(dbBooks book, ViewManager viewManager)
        {
            _book = book;
            _viewManager = viewManager;
        }

        public void OpenBook()
        {
            var bookView = _viewManager.GetView<PopupBook>();
            InitializeBookView(bookView);
            _viewManager.OpenView(bookView);
        }

        private void InitializeBookView(PopupBook view)
        {
            var cards = GetCardsFish(_currentTypeFish, _currentPage);

            view.SwitchArrows(_currentPage != 1, cards.Count == 4);
            view.ArrowLeft.AddListener(ClickLeft);
            view.ArrowRight.AddListener(ClickRight);
            // view.OnClickFish += HandlerClickFish;
            view.OnViewDestroyed += DestroyListeners;
            view.OnChooseFish += ChooseFish;
            view.OnChooseTab += ChooseTab;
            view.Initialize(cards, _currentTypeFish);
            return;

            void DestroyListeners()
            {
                view.ArrowLeft.RemoveAllListeners();
                view.ArrowRight.RemoveAllListeners();
            }

            void ClickLeft()
            {
                if (_currentPage == 1) return;
                _currentPage--;
                cards = GetCardsFish(_currentTypeFish, _currentPage);
                view.Initialize(cards, _currentTypeFish);
                view.SwitchArrows(_currentPage != 1, cards.Count == 4);
            }

            void ClickRight()
            {
                if (cards.Count != 4) return;
                _currentPage++;
                cards = GetCardsFish(_currentTypeFish, _currentPage);
                view.Initialize(cards, _currentTypeFish);
                view.SwitchArrows(_currentPage != 1, cards.Count == 4);
            }

            void ChooseFish(string fishName)
            {
                var info = GetFishInfo(fishName);
                view.OpenInfoFish(info);
            }

            void ChooseTab(TypeFish type)
            {
                _currentTypeFish = type;
                _currentPage = 1;
                cards = GetCardsFish(_currentTypeFish, _currentPage);
                view.Initialize(cards, _currentTypeFish);
                view.SwitchArrows(_currentPage != 1, cards.Count == 4);
            }
        }

        // private void HandlerClickFish() => _gameManager.Audio.Play(SoundType.ButtonClick, SoundCategory.Sfx);

        private List<CardFishIcon> GetCardsFish(TypeFish typeFish, int page = 1)
        {
            const int pageSize = 4;
            var cards = _book.GetCardFishByType(typeFish);
            return cards.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        private CollectionInfo GetFishInfo(string fishName)
        {
            var info = _book.GetCardFishById(_currentTypeFish, fishName);
            if (info == null) return default;

            var isPlayers = _currentTypeFish == TypeFish.Player;
            return new CollectionInfo
            {
                FishName = info.Name,
                FishDescription = info.Description,
                FishGameImage = info.GameIcon,
                IsPlayersFish = isPlayers,
                Health = isPlayers ? ((PlayerPage)info).Health : 0,
                Speed = isPlayers ? ((PlayerPage)info).Speed : ((EnemyPage)info).Speed,
                Damage = isPlayers ? 0 : ((EnemyPage)info).Damage,
            };
        }
    }
}