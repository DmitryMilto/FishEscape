using UnityEngine;
using Zenject;

public class FishManager : MonoBehaviour
{
    [Inject]
    private dbAllFish allfish;
    [Inject]
    private GameConfige gameConfige;

    private void Awake()
    {
        allfish.LoadData();
        gameConfige.chooseFish = allfish.Player[0];
    }

    private void OnDisable()
    {
        allfish.SaveData();
    }
}
