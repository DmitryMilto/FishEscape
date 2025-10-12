using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.HUD
{
    public class LivesView : UIPanelBase
    {
        [SerializeField] private Image[] hearts;

        public void UpdateLives(int lives)
        {
            for (int i = 0; i < hearts.Length; i++)
                hearts[i].enabled = i < lives;
        }
    }
}