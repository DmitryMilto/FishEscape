using FishEscape.Fishs;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllFish", menuName = "Fish/Fish/All", order = 1)]
public class dbAllFish : ScriptableObject
{
    [SerializeField]
    private List<PlayerFish> fishs;

    [SerializeField]
    private List<EnemyFish> enemies;

    public List<PlayerFish> Player => fishs;
    public List<EnemyFish> Enemy => enemies;

    [Button]
    private void SortPlayer()
    {
        fishs.Sort(delegate (PlayerFish x, PlayerFish y)
        {
            if (x == null && y == null) return 0;
            else if (x == null) return -1;
            else if (y == null) return 1;
            else
                return x.MaxPazzle.CompareTo(y.MaxPazzle);
        });
    }

    public void LoadData()
    {
        foreach(var fish in fishs)
        {
            fish.LoadData();
        }
        foreach(var enemy in enemies)
        {
            enemy.LoadData();
        }
    }
    public void SaveData()
    {
        foreach (var fish in fishs)
        {
            fish.SaveData();
        }
        foreach (var enemy in enemies)
        {
            enemy.SaveData();
        }
    }

    [Button]
    public List<EnemyFish> Enemies(EnumOcean ocean)
    {
        return Enemy.FindAll(x => x.Habitat == ocean);
    }
}
