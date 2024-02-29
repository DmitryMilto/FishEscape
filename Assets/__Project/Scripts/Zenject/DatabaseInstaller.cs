using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DatabaseInstaller", menuName = "Installers/DatabaseInstaller")]
public class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller>
{
    [SerializeField]
    private GameConfige gameConfig;
    [SerializeField]
    private dbAllFish allFish;
    public override void InstallBindings()
    {
        Container.BindInstance(gameConfig).AsSingle();
        Container.BindInstance(allFish).AsSingle();
    }
}