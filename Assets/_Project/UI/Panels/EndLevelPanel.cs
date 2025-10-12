using UnityEngine;
using UnityEngine.UI;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class EndLevelPanel : UIPanelBase
    {
        [SerializeField] private Button toMenuButton;

        protected void Awake()
        {
            toMenuButton.onClick.AddListener(OnToMenuClicked);
        }

        private void OnToMenuClicked()
        {
            TDebug.Log("[EndLevelPanel] Returning to menu...");
            // Тут загрузи сцену или покажи MainMenuPanel
        }
    }
}