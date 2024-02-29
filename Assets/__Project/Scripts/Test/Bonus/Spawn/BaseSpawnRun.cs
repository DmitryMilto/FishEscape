using Scripts.Bonus.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSpawnRun<T> : MonoBehaviour
{
    [SerializeField]
    protected int countBuble;

    [SerializeField]
    protected List<EmptyDatabaseBonus<T>> database;
}
