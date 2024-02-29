using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class TargetMap : MonoBehaviour
{
    #region Init Parament
    [Title("Database Ocean")]
    [SerializeField]
    [OnValueChanged("SetOcean")]
    private dbOcean ocean;

    [SerializeField]
    [EnumToggleButtons]
    private EnumTargetActive active;
    public EnumTargetActive Active { get { return active; } }

    [SerializeField]
    [EnumToggleButtons]
    private EnumOcean typeOcean;
    public EnumOcean Type
    {
        get
        {
            return typeOcean;
        }
    }

    [Title("Camera")]
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineVirtualCamera VirtualCamera
    { get 
        { 
            if(virtualCamera == null)
                virtualCamera = GetComponentInParent<CinemachineVirtualCamera>();
            return virtualCamera; 
        } 
    }

    [Title("Other")]
    [SerializeField]
    private GameObject arraw;
    private SpriteRenderer card;
    private SpriteRenderer Card
    {
        get
        {
            if(card == null)
                card = GetComponentInChildren<SpriteRenderer>();
            return card;
        }
    }

    [SerializeField]
    [ReadOnly]
    private MapSystem mapSystem;
    private MapSystem MapSystem
    {
        get
        {
            if(mapSystem == null) mapSystem = GetComponentInParent<MapSystem>();
            return mapSystem;
        }
    }

    private DiscoveryFish discoveryFish;
    private DiscoveryFish DiscoveryFish
    {
        get
        {
            if(discoveryFish == null)
                discoveryFish = GetComponentInChildren<DiscoveryFish>();
            return discoveryFish;
        }
    }

    [Inject]
    private GameConfige gameConfige;
    #endregion

    #region Public Methods
    public void SetOcean(dbOcean ocean)
    {
        this.ocean = ocean;
        if (active == EnumTargetActive.Active)
            this.Card.sprite = this.ocean.activeIcon;
        if (active == EnumTargetActive.Deactive && this.ocean.deactiveIcon != null)
            this.Card.sprite = this.ocean.deactiveIcon;
        ActiveOrDeactiveCamera(active == EnumTargetActive.Active);
        //arraw.SetActive(MapSystem.OpenArraw);
    }
    public void ActiveArraw()
    {
        arraw.SetActive(MapSystem.OpenArraw);
    }
    public void ActiveOrDeactiveCamera(bool active)
    {
        if (active)
            VirtualCamera.Priority = 1;
        else
            VirtualCamera.Priority = 0;
        //VirtualCamera.gameObject.SetActive(active);
        //VirtualCamera.enabled = active;
    }
    public void SwitchTarget(EnumPoint point)
    {
        MapSystem.SwitchLevel(point);
        switch (point)
        {
            case EnumPoint.Left: Left(); break;
            case EnumPoint.Right: Right(); break;
            case EnumPoint.Center: Center(); break;
            default: break;
        }
    }
    public void Left()
    {
        Debug.Log($"{this.name} click Left");
    }
    public void Right()
    {
        Debug.Log($"{this.name} click Right");
    }
    public void Center()
    {
        Debug.Log($"{this.name} click Center");
        gameConfige.Ocean = this.Type;
        SceneManager.LoadScene("Run", LoadSceneMode.Single);
    }
    #endregion

    #region Editor
    private void OnValidate()
    {
        this.name = this.Type.ToString();
    }
    #endregion
}
