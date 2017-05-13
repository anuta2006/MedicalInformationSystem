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
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Foundation.Cryptography;
using MedicalInformationSystem.Foundation.Authentication;
using MedicalInformationSystem.Foundation.Users;
using MedicalInformationSystem.Foundation.Factories;
using MedicalInformationSystem.Services.DataContracts;
using MedicalInformationSystem.Foundation.Providers;
using MedicalInformationSystem.UI.Factories;
using MedicalInformationSystem.UI.ViewModels.Class;
using MedicalInformationSystem.Foundation.MediaData;
using MedicalInformationSystem.Foundation.Controllers;
using MedicalInformationSystem.Foundation.Comparers;
using System.Collections.Generic;
using MedicalInformationSystem.UI.ViewModels.Student;
using MedicalInformationSystem.UI.ViewModels.Vaccination;
using MedicalInformationSystem.UI.ViewModels.DiseaseGroup;

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

            Container.RegisterType<ICryptographicRng, CryptographicRng>(new ContainerControlledLifetimeManager());
            Container.RegisterType<ISha512Cryptographer, Sha512Cryptographer>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAuthenticationCryptographer, AuthenticationCryptographer>(new ContainerControlledLifetimeManager());


            Container.RegisterType<IEqualityComparer<StudentData>, StudentDataEqualityComparer>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEqualityComparer<ClassData>, ClassDataEqualityComparer>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEqualityComparer<VaccinationData>, VaccinationDataEqualityComparer>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEqualityComparer<DiseaseGroupData>, DiseaseGroupEqualityComparer>(new ContainerControlledLifetimeManager());


            Container.RegisterType<IEntityControllerFactory<StudentData, IStudentController>, StudentControllerFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityControllerFactory<ClassData, IClassController>, ClassControllerFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityControllerFactory<VaccinationData, IVaccinationController>, VaccinationControllerFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityControllerFactory<DiseaseGroupData, IDiseaseGroupController>, DiseaseGroupControllerFactory>(new ContainerControlledLifetimeManager());


            Container.RegisterType<IEntityControllerProvider<StudentData, IStudentController>, CachingEntityControllerProvider<StudentData, IStudentController>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityControllerProvider<ClassData, IClassController>, CachingEntityControllerProvider<ClassData, IClassController>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityControllerProvider<VaccinationData, IVaccinationController>, CachingEntityControllerProvider<VaccinationData, IVaccinationController>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IEntityControllerProvider<DiseaseGroupData, IDiseaseGroupController>, CachingEntityControllerProvider<DiseaseGroupData, IDiseaseGroupController>>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IControllerViewModelFactory<IStudentController, StudentViewModel>, StudentViewModelFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IControllerViewModelFactory<IClassController, ClassInfoViewModel>, ClassInfoViewModelFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IControllerViewModelFactory<IVaccinationController, VaccinationViewModel>, VaccinationViewModelFactory>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IControllerViewModelFactory<IDiseaseGroupController, DiseaseGroupViewModel>, DiseaseGroupViewModelFactory>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IControllerViewModelProvider<IStudentController, StudentViewModel>, CachingControllerViewModelProvider<IStudentController, StudentViewModel>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IControllerViewModelProvider<IClassController, ClassInfoViewModel>, CachingControllerViewModelProvider<IClassController, ClassInfoViewModel>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IControllerViewModelProvider<IVaccinationController, VaccinationViewModel>, CachingControllerViewModelProvider<IVaccinationController, VaccinationViewModel>>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IControllerViewModelProvider<IDiseaseGroupController, DiseaseGroupViewModel>, CachingControllerViewModelProvider<IDiseaseGroupController, DiseaseGroupViewModel>>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IAuthenticationService, AuthenticationService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUserSettingsService, UserSettingsService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMedicalInformationService, MedicalInformationService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IAccountService, AccountService>(new ContainerControlledLifetimeManager());

            Container.RegisterInstance(new Lazy<IAccountService>(() => Container.Resolve<IAccountService>()));

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
