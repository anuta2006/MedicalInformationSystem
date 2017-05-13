using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services;
using MedicalInformationSystem.UI.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Windows.Input;

namespace MedicalInformationSystem.UI.ViewModels.StartUp
{
    [UsedImplicitly]
    public class SignInViewModel : BindableBase, INavigationAware
    {
        private readonly IAuthenticationService _authenticationService;
        private IRegionNavigationService _regionNavigationService;

        private string _login;
        private string _password;
        private string _errorMessage;
        private bool _isSigningIn;

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                SetProperty(ref _login, value);
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                SetProperty(ref _password, value);
            }
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                SetProperty(ref _errorMessage, value);
            }
        }

        public bool IsSigningIn
        {
            get
            {
                return _isSigningIn;
            }
            set
            {
                SetProperty(ref _isSigningIn, value);
            }
        }

        public ICommand GoBackCommand { get; }

        public ICommand SignInCommand { get; }

        public SignInViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            GoBackCommand = new DelegateCommand(GoBack);
            SignInCommand = new DelegateCommand(SignIn);
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
            => _regionNavigationService = navigationContext.NavigationService;

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
            => true;

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        private void GoBack()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.LandingView);
        }

        private async void SignIn()
        {
            IsSigningIn = true;
            var authorizationResult = await _authenticationService.SignInAsync(Login, Password);
            if (authorizationResult == AuthorizationResult.Success)
            {
                _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.MainView);
            }
            else
            {
                switch (authorizationResult)
                {
                    case AuthorizationResult.UserWithSuchLoginDoesNotExist:
                        ErrorMessage = ErrorMessages.UserDoesNotExist;
                        break;
                    case AuthorizationResult.IncorrectPassword:
                        ErrorMessage = ErrorMessages.IncorrectPassword;
                        break;
                    case AuthorizationResult.Failed:
                        ErrorMessage = ErrorMessages.AuthorizationFailed;
                        break;
                    default:
                        throw new ArgumentException("Unknown authorization result", nameof(authorizationResult));
                }
            }
            IsSigningIn = false;
        }

        private static class ErrorMessages
        {
            public const string UserDoesNotExist = "Пользователь с таким логином не существует";
            public const string IncorrectPassword = "Неверный пароль";
            public const string AuthorizationFailed = "Ошибка авторизации";
        }
    }
}
