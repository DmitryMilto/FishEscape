using FishEscape.Enums.Players;
using FishEscape.Fishs;
using Scripts.Card.Collection;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ControllerPageFish : UIContainerControllerBase
{
    [SerializeField]
    private Transform container;

    [Title("Prefabs")]
    [SerializeField]
    private PlayerCardCollection playerCards;

    [SerializeField]
    private EnemyCardCollection enemyCards;

    [SerializeField]
    private ControllerBook book;

    [SerializeField]
    private TypeFish fish;

    [ReadOnly]
    [SerializeField]
    private int maxCardFish = 9;

    [Inject]
    private dbAllFish allFish;

    protected override void HideCallback()
    {
        Clear(container);
    }

    protected override void ShowCallback()
    {
        if (this.fish == TypeFish.Player)
            CreatePlayerFish();
        else
            CreateEnemyFish();
    }

    private void CreateCard<T>(BaseCardCollection<T> prefabs, T fish) where T : Fish
    {
        var item = Instantiate(prefabs);
        item.transform.SetParent(container);
        item.transform.localScale = Vector3.one;
        if (fish != null)
        {
            item.SetCard(fish);
            item.SetPageCollection(book);
        }
    }

    private void CreatePlayerFish()
    {
        foreach (var fish in allFish.Player)
        {
            CreateCard(playerCards, fish);
        }
        for(int i = 0; i < maxCardFish - allFish.Player.Count; i++)
        {
            CreateCard(playerCards, null);
        }
    }
    private void CreateEnemyFish()
    {
        foreach (var fish in allFish.Enemy)
        {
            CreateCard(enemyCards, fish);
        }
        for (int i = 0; i < maxCardFish - allFish.Enemy.Count; i++)
        {
            CreateCard(enemyCards, null);
        }
    }
    
}
