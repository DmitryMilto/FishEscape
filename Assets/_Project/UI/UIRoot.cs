using UnityEngine;
using VContainer;
using _Project.UI.Panels;
using _Project.Core.Utils;

namespace _Project.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private UIViewLocator viewLocator;

        private UINavigationService _navigation;

        [Inject]
        public void Construct(UINavigationService navigation)
        {
            _navigation = navigation;
        }

        private void Start()
        {
            if (viewLocator == null)
            {
                TDebug.LogError("[UIRoot] UIViewLocator not assigned.");
                return;
            }

            // Показываем стартовую панель
            _navigation.Open<MainMenuPanel>();
            TDebug.Log("[UIRoot] MainMenuPanel shown on start.");
        }
    }
}