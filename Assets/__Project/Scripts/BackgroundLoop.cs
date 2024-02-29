using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    private PoolMono<InitializingMap> poolObject;
    [SerializeField] private InitializingMap prefabsMap;

    [SerializeField]
    private int countActiveMap = 3;
    [SerializeField]
    private float speed = 0.01f;
    public float Speed => speed;

    [ReadOnly]
    [SerializeField]
    private List<InitializingMap> activeMap;

    private Vector3 startPostion = new Vector3(20.9f, 0, 0);
    private Vector3 increasePositiion = new Vector3(63.8f, 0, 0);
    private void Awake()
    {
        if (prefabsMap == null) return;
        poolObject = new PoolMono<InitializingMap>(prefabsMap, countActiveMap, this.transform);
        CreateMap();
        CreateMap();

    }
    [Button]
    public void CreateMap()
    {
        var map = this.poolObject.GetFreeElement();
        if(map == null) return;
        if (activeMap.Count == 0)
        {
            map.transform.position = startPostion;
        }
        else
        {
            map.transform.position = activeMap[activeMap.Count - 1].transform.position + increasePositiion;
        }
        activeMap.Add(map);
        map.gameObject.SetActive(true);
    }
    public void RemoveActiveMap(InitializingMap map)
    {
        activeMap.Remove(map);
    }
}
