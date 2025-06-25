using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.Views.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.GameLifes
{
    public class GameLifetimeScope: LifetimeScope
    {
        [SerializeField] private ViewRegistry viewRegistry;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            // Регистрируем ViewRegistry из ресурсов
            builder.RegisterInstance(viewRegistry);

            // Регистрируем ViewManager как singleton-компонент
            var viewManagerPrefab = Resources.Load<ViewManager>("ViewManager");
            builder.RegisterComponentInNewPrefab<ViewManager>(viewManagerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();

            // Пример: регистрация других сервисов
            // builder.Register<SomeService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<AppData>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}