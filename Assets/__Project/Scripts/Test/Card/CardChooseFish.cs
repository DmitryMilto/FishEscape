using Doozy.Runtime.UIManager.Components;
using FishEscape.Fishs;
using UnityEngine;
using UnityEngine.UI;

public class CardChooseFish : MonoBehaviour
{
    [SerializeField] private PlayerFish fish;
    [Space]
    [SerializeField] private Image imageFish;
    [Space]
    [SerializeField] private UIToggle toggle;

    public void SetPlayerFish(PlayerFish fish, UIToggleGroup group)
    {
        if (fish == null) return;
        this.fish = fish;
        imageFish.sprite = this.fish.fish;
        toggle.AddToToggleGroup(group);
    }
}
