using UnityEngine;

public class FishManager : MonoBehaviour
{

    private dbAllFish allfish;

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
