using System;
using System.Reflection;
using System.Windows;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Unity;
using MedicalInformationSystem.Services;
using MedicalInformationSystem.Services.Interfaces;
using MedicalInformationSystem.UI;
using MedicalInformationSystem.WpfProject.Navigation;
using MedicalInformationSystem.WpfProject.ViewViewModelTypeResolver;

namespace MedicalInformationSystemAgent
{
    internal class MedicalServiceBootstrapper : UnityBootstrapper
    {
        private readonly IViewViewModelTypeResolver _viewViewModelTypeResolver;

        public MedicalServiceBootstrapper()
        {
            var uiAssembly = typeof(MedicalInformationSystemMainModule).GetTypeInfo().Assembly;
            _viewViewModelTypeResolver = new ViewViewModelTypeResolver(uiAssembly);
        }

        protected override DependencyObject CreateShell()
            => new Shell();

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(MedicalInformationSystemMainModule));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterInstance(_viewViewModelTypeResolver);

            Container.RegisterType<IMedicalInformationService, MedicalInformationService>(new ContainerControlledLifetimeManager());

            RegionManagerContext.Current = Container.Resolve<IRegionManager>();

            ViewModelLocationProvider.SetDefaultViewModelFactory(Resolve);
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(GetViewModelType);
        }

        private object Resolve(Type type)
            => Container.Resolve(type);

        private Type GetViewModelType(Type viewType)
            => _viewViewModelTypeResolver.GetViewModelType(viewType);
    }
}
