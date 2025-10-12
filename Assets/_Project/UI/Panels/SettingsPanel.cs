using UnityEngine;
using UnityEngine.UI;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class SettingsPanel : UIPanelBase
    {
        [SerializeField] private Button closeButton;
        [SerializeField] private Toggle soundToggle;
        [SerializeField] private Toggle vibrationToggle;

        private void Awake()
        {
            closeButton.onClick.AddListener(OnCloseClicked);
            soundToggle.onValueChanged.AddListener(OnSoundToggleChanged);
            vibrationToggle.onValueChanged.AddListener(OnVibrationToggleChanged);
        }

        private void OnCloseClicked()
        {
            TDebug.Log("[SettingsPanel] Close clicked");
            Hide();
        }

        private void OnSoundToggleChanged(bool value)
        {
            TDebug.Log($"[SettingsPanel] Sound toggled: {value}");
            // TODO: Apply sound setting
        }

        private void OnVibrationToggleChanged(bool value)
        {
            TDebug.Log($"[SettingsPanel] Vibration toggled: {value}");
            // TODO: Apply vibration setting
        }
    }
}