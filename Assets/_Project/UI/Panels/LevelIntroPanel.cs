using UnityEngine;
using _Project.Core.Utils;

namespace _Project.UI.Panels
{
    public class LevelIntroPanel : MonoBehaviour
    {
        public void Show(string levelName)
        {
            TDebug.Log($"[LevelIntroPanel] Showing level: {levelName}");
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}