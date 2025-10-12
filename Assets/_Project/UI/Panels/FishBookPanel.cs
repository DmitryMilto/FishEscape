using UnityEngine;
using UnityEngine.UI;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class FishBookPanel : UIPanelBase
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Transform fishListContainer;
        [SerializeField] private GameObject fishItemPrefab;

        private void Awake()
        {
            closeButton.onClick.AddListener(OnCloseClicked);
        }

        private void OnCloseClicked()
        {
            TDebug.Log("[FishBookPanel] Close clicked");
            Hide();
        }

        public void PopulateFishBook()
        {
            TDebug.Log("[FishBookPanel] Populating fish list...");
            // TODO: Populate fish from data (FishDataCollection, etc.)
        }
    }
}