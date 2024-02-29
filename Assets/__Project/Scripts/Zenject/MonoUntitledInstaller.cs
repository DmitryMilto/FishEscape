using UnityEngine;
using Zenject;

public class MonoUntitledInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        InstallSaveManager();
    }
    private void InstallSaveManager()
    {
        var manager = new SaveSystem();
        Container.BindInstance(manager);
    }
}