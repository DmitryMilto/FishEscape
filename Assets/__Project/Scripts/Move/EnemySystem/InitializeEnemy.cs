using FishEscape.Fishs;
using Sirenix.OdinInspector;
using UnityEngine;

public class InitializeEnemy : MonoBehaviour
{
    [OnValueChanged("Init")]
    [SerializeField] private EnemyFish enemy;

    private void InitNull()
    {
        if (enemy == null) return;
    }

    public void Init(EnemyFish enemy)
    {
        this.enemy = enemy;
    }
}
