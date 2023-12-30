using Doozy.Runtime.UIManager.Components;
using FishEscape.Fishs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class CardSystem : MonoBehaviour
{
    [Inject]
    private GameConfige gameConfige;

    [SerializeField] private List<PlayerFish> fishs;
    [Space]
    [SerializeField] private Transform container;
    [SerializeField] private CardChooseFish _toggle;

    [Space]
    [SerializeField] private UIToggleGroup toggleGroup;

    [Space]
    [SerializeField] private UIButton button;

    private void Start()
    {
        button.onClickEvent.AddListener(ChooseFish);
        CreateCard();
    }

    private void CreateCard()
    {
        foreach (var fish in fishs)
        {
            var card = Instantiate(_toggle);
            card.gameObject.transform.SetParent(container, false);
            card.transform.localScale = Vector3.one;
            card.SetPlayerFish(fish, toggleGroup);
        }
    }

    private void ChooseFish()
    {
        Debug.Log($"{toggleGroup.lastToggleOnIndex}");
        gameConfige.chooseFish = fishs[toggleGroup.lastToggleOnIndex];
        SceneManager.LoadScene("Run");
    }
}
