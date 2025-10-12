using VContainer;
using VContainer.Unity;
using UnityEngine;
using _Project.UI;
using _Project.UI.Panels;
using _Project.UI.HUD;

namespace _Project.Installers
{
    public class UISceneInstaller : LifetimeScope
    {
        [SerializeField] private UIViewLocator viewLocator;
        [SerializeField] private UIRoot uiRoot;

        [Header("Panels")]
        [SerializeField] private MainMenuPanel mainMenuPanel;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private FishBookPanel fishBookPanel;
        [SerializeField] private ShopPanel shopPanel;

        [Header("HUD")]
        [SerializeField] private LivesView livesView;
        [SerializeField] private BoostsPanel boostsPanel;
        [SerializeField] private AbilitiesPanel abilitiesPanel;
        [SerializeField] private PauseMenu pauseMenu;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(viewLocator);
            builder.Register<UINavigationService>(Lifetime.Singleton);
            
            builder.RegisterInstance(uiRoot);
            
            builder.RegisterInstance(mainMenuPanel);
            builder.RegisterInstance(settingsPanel);
            builder.RegisterInstance(fishBookPanel);
            builder.RegisterInstance(shopPanel);

            builder.RegisterInstance(livesView);
            builder.RegisterInstance(boostsPanel);
            builder.RegisterInstance(abilitiesPanel);
            builder.RegisterInstance(pauseMenu);
            
            builder.RegisterInstance(viewLocator.EndLevelPanel).As<EndLevelPanel>();
        }
    }
}