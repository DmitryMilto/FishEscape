using UnityEngine;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class GameOverPanel : MonoBehaviour
    {
        public void Show()
        {
            TDebug.Log("[GameOverPanel] Showing Game Over");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}