using FishEscape.Fishs;
using Sirenix.OdinInspector;
using UnityEngine;

public class InitializeEnemy : MonoBehaviour
{
    [OnValueChanged("Init")]
    [SerializeField] private EnemyFish enemy;

    private EnemyMono enemyMono;
    private void InitNull()
    {
        if (enemy == null) return;
    }

    public void Init(EnemyFish enemy)
    {
        this.enemy = enemy;
        CreateFish();
    }
    private void CreateFish()
    {
        if(this.enemy == null) return;
        if(enemyMono != null) return;
        enemyMono = this.gameObject.AddComponent<EnemyMono>();
        enemyMono.Init(enemy);
        enemyMono.gameObject.SetActive(true);
    }
    private void OnDisable()
    {
        Destroy(enemyMono);
        enemyMono = null;
    }
}
