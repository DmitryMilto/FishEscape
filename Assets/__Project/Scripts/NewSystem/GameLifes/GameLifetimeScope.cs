using __Project.Scripts.NewSystem.Controllers.Audios;
using __Project.Scripts.NewSystem.Controllers.DataManager;
using __Project.Scripts.NewSystem.Database;
using __Project.Scripts.NewSystem.Database.Audios;
using __Project.Scripts.NewSystem.Database.Fishes;
using __Project.Scripts.NewSystem.Database.View;
using __Project.Scripts.NewSystem.DataManager;
using __Project.Scripts.NewSystem.DataManager.Providers;
using __Project.Scripts.NewSystem.Enums;
using __Project.Scripts.NewSystem.Views.Managers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace __Project.Scripts.NewSystem.GameLifes
{
    public class GameLifetimeScope: LifetimeScope
    {
        [SerializeField] private SoundDatabase soundDatabase;
        [SerializeField] private ViewRegistry viewRegistry;
        
        [Header("ScriptableObjects")]
        [SerializeField] private PlayersDataFish playersDataFish;
        [SerializeField] private EnemyDataFish enemyDataFish;
        [SerializeField] private BoosterDataFish boosterDataFish;
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            // Регистрируем ViewRegistry из ресурсов
            builder.RegisterInstance(viewRegistry);

            // Регистрируем ViewManager как singleton-компонент
            var viewManagerPrefab = Resources.Load<ViewManager>("ViewManager");
            builder.RegisterComponentInNewPrefab<ViewManager>(viewManagerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad();

            // Загрузка и регистрация AudioController через ресурсы
            var audioControllerPrefab = Resources.Load<AudioController>("AudioController");
            builder.RegisterComponentInNewPrefab<AudioController>(audioControllerPrefab, Lifetime.Singleton)
                .DontDestroyOnLoad()
                .AsImplementedInterfaces();

            builder.Register<AppData>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<LevelsProvider>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            
            var fishBookDatabase = Resources.Load<FishBookDatabase>("FishBookDatabase");
            builder.RegisterInstance(fishBookDatabase); // ScriptableObject с данными
            builder.Register<FishBookManager>(Lifetime.Singleton).AsSelf();
            
            builder.Register<IFileManager, FileManager>(Lifetime.Singleton);

            InjectScriptableObjects(builder);
            
            builder.RegisterInstance(soundDatabase);
        }

        private void InjectScriptableObjects(IContainerBuilder builder)
        {
            builder.RegisterInstance(playersDataFish);
            builder.RegisterInstance(enemyDataFish);
            builder.RegisterInstance(boosterDataFish);
        }
    }
}