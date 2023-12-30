using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class GameManager : MonoBehaviour
{
    [Inject]
    private GameConfige confige;
    [Title("Player")]
    [SerializeField]
    private InitializePlayer player;

    [Title("Healht System")]
    [SerializeField]
    private float healht;

    [Title("Enemy System")]
    [SerializeField]
    private SpawnEnemy enemy;


    private void Awake()
    {
        player.SetPlayer(confige.chooseFish);
        enemy.SetEnemy(confige.chooseEnemy);
    }
}
