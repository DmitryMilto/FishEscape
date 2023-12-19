using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab;
    public bool autoExpant { get; set; }

    public Transform container { get; }

    private List<T> pool;

    public PoolMono(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;
    }
    public PoolMono(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;

        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createObject = Object.Instantiate(this.prefab);
        createObject.gameObject.SetActive(isActiveByDefault);
        createObject.gameObject.transform.SetParent(this.container);
        this.pool.Add(createObject);
        return createObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                return true;
            }
        }
        element = null;
        return false;
    }
    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
            return element;
        else
        if (this.autoExpant)
            return CreateObject(true);
        else return null;

        throw new System.Exception($"There is not free elements in pool of type {typeof(T)}");
    }
}
