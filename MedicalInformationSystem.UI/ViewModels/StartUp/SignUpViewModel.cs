using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services;
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
    public class SignUpViewModel : BindableBase, INavigationAware
    {
        private readonly IAuthenticationService _authenticationService;

        private IRegionNavigationService _regionNavigationService;

        private string _login;
        private string _password;
        private string _confirmPassword;
        private string _firstName;
        private string _lastName;
        private string _patronymic;
        private string _qualification;
        private string _errorMessage;
        private bool _isSigningUp;

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

        public string ConfirmPassword
        {
            get
            {
                return _confirmPassword;
            }
            set
            {
                SetProperty(ref _confirmPassword, value);
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                SetProperty(ref _firstName, value);
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                SetProperty(ref _lastName, value);
            }
        }

        public string Patronymic
        {
            get
            {
                return _patronymic;
            }
            set
            {
                SetProperty(ref _patronymic, value);
            }
        }

        public string Qualification
        {
            get
            {
                return _qualification;
            }
            set
            {
                SetProperty(ref _qualification, value);
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

        public bool IsSigningUp
        {
            get
            {
                return _isSigningUp;
            }
            set
            {
                SetProperty(ref _isSigningUp, value);
            }
        }

        public ICommand GoBackCommand { get; }

        public ICommand SignUpCommand { get; }

        public SignUpViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            GoBackCommand = new DelegateCommand(GoBack);
            SignUpCommand = new DelegateCommand(SignUp);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
            => _regionNavigationService = navigationContext.NavigationService;

        private void GoBack()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.LandingView);
        }

        private async void SignUp()
        {
            if (Password != ConfirmPassword)
            {
                ErrorMessage = ErrorMessages.PasswordsDoNotMatch;

                return;
            }

            IsSigningUp = true;
            ErrorMessage = String.Empty;

            var registrationResult = await _authenticationService.SignUpAsync(Login, Password, FirstName, LastName, Patronymic, Qualification);
            if (registrationResult == RegistrationResult.Success)
            {
                _regionNavigationService.Navigate(MedicalInformationSystemRegions.MainRegion, MedicalInformationSystemViews.MainView);
            }
            else
            {
                switch (registrationResult)
                {
                    case RegistrationResult.Success:
                        break;
                    case RegistrationResult.UserWithSuchLoginAlreadyExists:
                        ErrorMessage = ErrorMessages.UserAlreadyExists;
                        break;
                    case RegistrationResult.Failed:
                        ErrorMessage = ErrorMessages.RegistrationFailed;
                        break;
                    default:
                        throw new ArgumentException("Unknown registration result", nameof(registrationResult));
                }
            }

            IsSigningUp = false;
        }


        private static class ErrorMessages
        {
            public const string UserAlreadyExists = "Пользователь с таким логино уже существует";
            public const string PasswordsDoNotMatch = "Пароли не совпадают";
            public const string RegistrationFailed = "Ошибка регистрации";
        }
    }
}
