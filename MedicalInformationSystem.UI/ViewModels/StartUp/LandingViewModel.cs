using MedicalInformationSystem.Common;
using MedicalInformationSystem.UI.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedicalInformationSystem.UI.ViewModels.StartUp
{
    [UsedImplicitly]
    public class LandingViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;

        public ICommand SignUpCommand { get; }

        public ICommand SignInCommand { get; }

        public LandingViewModel()
        {
            SignUpCommand = new DelegateCommand(NavigateToSignUpView);
            SignInCommand = new DelegateCommand(NavigateToSignInView);
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
            => true;

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
            => _regionNavigationService = navigationContext.NavigationService;

        private void NavigateToSignUpView()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.SignUpView);
        }

        private void NavigateToSignInView()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.SignInView);
        }
    }
}
