using UnityEngine;

namespace _Project.UI.Panels
{
    public class MainMenuPanel : UIPanelBase
    {
        public void OnPlayPressed() => Debug.Log("Start Game");
        public void OnSettingsPressed() => Debug.Log("Open Settings");
        public void OnFishBookPressed() => Debug.Log("Open Fish Book");
        public void OnShopPressed() => Debug.Log("Open Shop");
    }
}