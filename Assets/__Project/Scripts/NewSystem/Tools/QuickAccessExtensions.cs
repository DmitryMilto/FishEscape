using UnityEngine.Events;
using UnityEngine.UI;

namespace __Project.Scripts.NewSystem.Tools
{
    public static class QuickAccessExtensions
    {
        //button
        public static void AddListener(this Button button, UnityAction listener) => button.onClick.AddListener(listener);
        public static void RemoveListener(this Button button, UnityAction listener) => button.onClick.RemoveListener(listener);
        public static void RemoveAllListeners(this Button button) => button.onClick.RemoveAllListeners();
    }
}