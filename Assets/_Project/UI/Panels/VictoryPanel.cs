using UnityEngine;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class VictoryPanel : MonoBehaviour
    {
        public void Show()
        {
            TDebug.Log("[VictoryPanel] Showing Victory");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}