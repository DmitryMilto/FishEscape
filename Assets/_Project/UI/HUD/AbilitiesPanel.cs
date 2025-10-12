using UnityEngine;
using UnityEngine.UI;

namespace _Project.UI.HUD
{
    public class AbilitiesPanel : UIPanelBase
    {
        [SerializeField] private Button[] abilityButtons;

        public void Init(System.Action<int> onAbilityClick)
        {
            for (int i = 0; i < abilityButtons.Length; i++)
            {
                int index = i;
                abilityButtons[i].onClick.AddListener(() => onAbilityClick?.Invoke(index));
            }
        }

        public void SetAbilityState(int index, bool enabled)
        {
            abilityButtons[index].interactable = enabled;
        }
    }
}