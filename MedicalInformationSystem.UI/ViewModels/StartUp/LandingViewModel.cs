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
    public class LandingViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;

        public ICommand SignUp { get; }

        public ICommand SignIn{ get; }

        public LandingViewModel()
        {
            SignIn = new DelegateCommand(NavigateToSignInView);
            SignUp = new DelegateCommand(NavigateToSignUpView);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;
        

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
            => _regionNavigationService = navigationContext.NavigationService;

        private void NavigateToSignInView()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.SignInView);
        }

        private void NavigateToSignUpView()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.SignUpView);
        }
    }
}
