using __Project.Scripts.NewSystem.Controllers.Audios;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Database;
using __Project.Scripts.NewSystem.Database.Audios;
using __Project.Scripts.NewSystem.Database.Books;
using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.DataManager.Providers;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Services;
using __Project.Scripts.NewSystem.Views.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.GameLifes
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private SoundDatabase soundDatabase;
        [SerializeField] private ViewRegistry viewRegistry;

        [Header("Game Objects Injection")] [SerializeField]
        private ViewManager viewManager;

        [SerializeField] private AudioController audioController;

        [Header("ScriptableObjects")] [SerializeField]
        private PlayersDataFish playersDataFish;

        [SerializeField] private EnemyDataFish enemyDataFish;
        [SerializeField] private BoosterDataFish boosterDataFish;
        [SerializeField] private dbBooks books;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            // Регистрируем ViewRegistry из ресурсов
            InjectScriptableObjects(builder);
            
            RegisterGameObjects(builder, viewManager);
            RegisterGameObjects(builder, audioController);

            builder.Register<AppData>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LevelsProvider>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<HomeRouting>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();

            builder.Register<IFileManager, FileManager>(Lifetime.Singleton);
            
            RegisterServices(builder);
        }

        private void RegisterGameObjects<T>(IContainerBuilder builder, T gameObject) where T : MonoBehaviour
        {
            builder.RegisterComponentInNewPrefab<T>(gameObject, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();
        }

        private void InjectScriptableObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(viewRegistry);
            builder.RegisterInstance(soundDatabase);
            builder.RegisterInstance(playersDataFish);
            builder.RegisterInstance(enemyDataFish);
            builder.RegisterInstance(boosterDataFish);
            builder.RegisterInstance(books);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<HomeDataService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<BookDataService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}