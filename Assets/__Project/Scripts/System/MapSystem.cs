using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapSystem : MonoBehaviour
{
    [Title("List database")]
    [SerializeField]
    private List<dbOcean> oceans;

    [Title("Targets")]
    [ReadOnly]
    [SerializeField]
    private List<TargetMap> allTargetMaps;

    [ReadOnly]
    [SerializeField]
    private List<TargetMap> activeMaps;

    [SerializeField]
    private int targetLevel = 0;
    private void Awake()
    {
        allTargetMaps = GetComponentsInChildren<TargetMap>().ToList();
    }
    private void Start()
    {
        foreach (var item in allTargetMaps)
        {
            var db = oceans.Find(x => x.Type == item.Type);
            item.SetOcean(db);

            if (item.Active == EnumTargetActive.Active)
            {
                activeMaps.Add(item);
            }
        }
        allTargetMaps.ForEach(x => x.ActiveArraw());
    }
    public void SwitchLevel(EnumPoint point)
    {
        if(point == EnumPoint.Left)
        {
            NextLevel();
        }
        else if(point == EnumPoint.Right)
        {
            PreviousLevel();
        }
    }
    public bool OpenArraw => activeMaps.Count > 1;
    private void NextLevel()
    {
        activeMaps[targetLevel].ActiveOrDeactiveCamera(false);
        if (targetLevel == activeMaps.Count - 1) targetLevel = 0;
        else targetLevel = Mathf.Clamp(++targetLevel, 0, activeMaps.Count - 1);
        activeMaps[targetLevel].ActiveOrDeactiveCamera(true);
    }
    private void PreviousLevel()
    {
        activeMaps[targetLevel].ActiveOrDeactiveCamera(false);
        if (targetLevel == 0) targetLevel = activeMaps.Count - 1;
        else targetLevel = Mathf.Clamp(--targetLevel, 0, activeMaps.Count - 1);
        activeMaps[targetLevel].ActiveOrDeactiveCamera(true);
    }
}
