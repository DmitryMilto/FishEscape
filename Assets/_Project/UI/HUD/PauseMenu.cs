using UnityEngine;

namespace _Project.UI.HUD
{
    public class PauseMenu : UIPanelBase
    {
        public void OnResumePressed() => Debug.Log("Resume");
        public void OnExitPressed() => Debug.Log("Exit to Menu");
    }
}