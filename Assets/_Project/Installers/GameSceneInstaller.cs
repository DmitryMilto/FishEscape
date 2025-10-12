using VContainer;
using VContainer.Unity;
using UnityEngine;
using _Project.Systems;
using _Project.Game.World;
using _Project.Game.Spawner;
using _Project.Game.Enemies;
using _Project.Game.Player;
using _Project.Game.Boosts;

namespace _Project.Installers
{
    public class GameSceneInstaller : LifetimeScope
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private GameStateMachine gameStateMachine;
        [SerializeField] private SpawnManager spawnManager;
        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private BoostManager boostManager;
        [SerializeField] private CollisionManager collisionManager;
        [SerializeField] private PlayerController playerController;

        protected override void Configure(IContainerBuilder builder)
        {
            // Менеджеры
            builder.RegisterInstance(gameManager);
            builder.RegisterInstance(gameStateMachine);
            builder.RegisterInstance(spawnManager);
            builder.RegisterInstance(enemySpawner);
            builder.RegisterInstance(boostManager);
            builder.RegisterInstance(collisionManager);

            // Игрок
            builder.RegisterInstance(playerController);
            
            builder.RegisterComponentInHierarchy<GameManager>().AsSelf().AsImplementedInterfaces();
        }
    }
}