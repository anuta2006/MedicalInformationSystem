using MedicalInformationSystem.UI.Views.StartUp;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;

namespace MedicalInformationSystem.UI
{
    public class MedicalInformationSystemMainModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public MedicalInformationSystemMainModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(LandingView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(SignUpView));
            _regionManager.RegisterViewWithRegion(MedicalInformationSystemRegions.MainRegion, typeof(SignInView));

            _regionManager.Regions[MedicalInformationSystemRegions.MainRegion].NavigationService.RequestNavigate(new Uri(MedicalInformationSystemViews.LandingView, UriKind.Relative));
        }
    }
}
