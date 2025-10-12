using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.HUD
{
    public class BoostsPanel : UIPanelBase
    {
        [SerializeField] private Image[] boostIcons;

        public void SetBoostIcon(int index, Sprite icon, float duration)
        {
            if (index >= 0 && index < boostIcons.Length)
            {
                boostIcons[index].sprite = icon;
                boostIcons[index].enabled = true;
                // TODO: добавить таймер UI
            }
        }
    }
}