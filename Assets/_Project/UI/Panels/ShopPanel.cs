using UnityEngine;
using UnityEngine.UI;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class ShopPanel : UIPanelBase
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Button buyGoldButton;
        [SerializeField] private Button buyCrystalsButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(OnCloseClicked);
            buyGoldButton.onClick.AddListener(OnBuyGold);
            buyCrystalsButton.onClick.AddListener(OnBuyCrystals);
        }

        private void OnCloseClicked()
        {
            TDebug.Log("[ShopPanel] Close clicked");
            Hide();
        }

        private void OnBuyGold()
        {
            TDebug.Log("[ShopPanel] Buying gold...");
            // TODO: Trigger CurrencyService for gold
        }

        private void OnBuyCrystals()
        {
            TDebug.Log("[ShopPanel] Buying crystals...");
            // TODO: Trigger CurrencyService for crystals
        }
    }
}