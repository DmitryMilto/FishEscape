using System.Collections.Generic;
using UnityEngine;

namespace __Project.Scripts.NewSystem.Pools
{
    public class AdvancedPoolMono<T> where T : MonoBehaviour
    {
        public T prefab;
        public int maxCount;
        public Transform container { get; }
        private List<T> pool;
        
        public List<T> Pool => pool;
        public T Prefab => prefab;

        public AdvancedPoolMono(T prefab, int maxCount, Transform container = null)
        {
            this.prefab = prefab;
            this.maxCount = maxCount;
            this.container = container;
            this.pool = new List<T>();
        }

        public T GetOrReuseElement(Vector3 newPosition)
        {
            // 1. Есть неактивный — возвращаем его
            foreach (var obj in pool)
            {
                if (!obj.gameObject.activeInHierarchy)
                {
                    obj.transform.position = newPosition;
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }
            // 2. Лимит не достигнут — создаём новый
            if (pool.Count < maxCount)
            {
                var newObj = Object.Instantiate(prefab, newPosition, Quaternion.identity, container);
                newObj.gameObject.SetActive(true);
                pool.Add(newObj);
                return newObj;
            }
            // 3. Лимит достигнут — переиспользуем первый неактивный (или любой)
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].gameObject.activeInHierarchy)
                {
                    pool[i].transform.position = newPosition;
                    pool[i].gameObject.SetActive(true);
                    return pool[i];
                }
            }
            // 4. Все активны — можно выбрать случайный и переместить (или ничего не делать)
            int randomIndex = Random.Range(0, pool.Count);
            pool[randomIndex].transform.position = newPosition;
            // Можно добавить сброс состояния объекта, если нужно
            return pool[randomIndex];
        }
        
        public int ActiveCount()
        {
            int count = 0;
            foreach (var obj in pool)
                if (obj.gameObject.activeInHierarchy) count++;
            return count;
        }
        public void DestroyAll()
        {
            foreach (var obj in pool)
            {
                if (obj != null)
                    Object.Destroy(obj.gameObject);
            }
            pool.Clear();
        }
    }
}