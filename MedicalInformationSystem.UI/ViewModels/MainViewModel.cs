using MedicalInformationSystem.Common;
using MedicalInformationSystem.UI.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;

namespace MedicalInformationSystem.UI.ViewModels
{
    [UsedImplicitly]
    public class MainViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;

        public MainViewModel()
        {

        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;

            _regionNavigationService.Navigate(MedicalInformationSystemRegions.ClassInfoRegion, MedicalInformationSystemViews.ClassInfoView);
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
            => true;

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
    }
}
