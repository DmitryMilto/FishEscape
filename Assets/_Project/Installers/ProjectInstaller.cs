using VContainer;
using VContainer.Unity;
using _Project.Services;
using _Project.Systems;

namespace _Project.Installers
{
    public class ProjectInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // Сервисы
            builder.Register<SaveService>(Lifetime.Singleton);
            builder.Register<CurrencyService>(Lifetime.Singleton);
            builder.Register<ProgressionService>(Lifetime.Singleton);
            builder.Register<LocalizationService>(Lifetime.Singleton);

            // Глобальные системы
            builder.Register<SoundManager>(Lifetime.Singleton);
            builder.Register<TimeScaleManager>(Lifetime.Singleton);
        }
    }
}