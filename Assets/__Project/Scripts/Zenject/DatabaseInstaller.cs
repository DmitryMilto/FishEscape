using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "DatabaseInstaller", menuName = "Installers/DatabaseInstaller")]
public class DatabaseInstaller : ScriptableObjectInstaller<DatabaseInstaller>
{
    [SerializeField]
    private GameConfige gameConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(gameConfig).AsSingle();
    }
}