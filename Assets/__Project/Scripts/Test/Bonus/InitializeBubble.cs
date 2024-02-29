using Scripts.Bonus.Database;
using Scripts.Bonus.Enum;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

public class InitializeBubble : MonoBehaviour
{
    [Button]
    public void Initialize(BonusDatabase bonus)
    {
        Debug.Log("Init Bonus");
        var obj = this.gameObject.AddComponent<BonusBubble>();
        obj.Initialize(bonus);
    }
    public void Initialize(PuzzleDatabase bonus)
    {
        var obj = this.gameObject.AddComponent<PuzzleBubble>();
        obj.Initialize(bonus);
    }
}
