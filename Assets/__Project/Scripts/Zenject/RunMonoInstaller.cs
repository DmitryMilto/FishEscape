using UnityEngine;
using Zenject;

public class RunMonoInstaller : MonoInstaller
{
    [Inject]
    private GameConfige gameConfige;
    public override void InstallBindings()
    {
        RunSystem();
    }

    private void RunSystem()
    {
        var health = new HealthSystem(gameConfige.chooseFish.health);
        Container.BindInstance<HealthSystem>(health);

        var speed = new SpeedSystem(gameConfige.chooseFish.speed);
        Container.BindInstance<SpeedSystem>(speed);
    }
}