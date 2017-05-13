using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicalInformationSystem.Services;
using MedicalInformationSystem.Foundation.Authentication;
using MedicalInformationSystem.Services.Interfaces;
using MedicalInformationSystem.Services.DataContracts;
using MedicalInformationSystem.Foundation.Users;
using MedicalInformationSystem.Common.Extensions;

namespace MedicalInformationSystem.Foundation.Authentication
{
    [UsedImplicitly]
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthenticationCryptographer _authenticatioCryptographer;
        private readonly IMedicalInformationService _medicalService;
        private readonly IUserSettingsService _userSettingsService;

        public event EventHandler SignedOut;

        public AuthenticationService(
            IAuthenticationCryptographer authenticationCryptographer,
            IMedicalInformationService medicalService,
            IUserSettingsService userSettingsService)
        {
            _authenticatioCryptographer = authenticationCryptographer;
            _medicalService = medicalService;
            _userSettingsService = userSettingsService;
        }

        public async Task<AuthorizationResult> SignInAsync(string login, string password)
        {
            UserDoctorData userDoctorData;
            UserData userData;
            try
            {
                userDoctorData = await _medicalService.GetUserDoctorDataAsync(login);
                userData = await _medicalService.GetUserDataAsync(login);
            }
            catch (InvalidOperationException e)
            {
                return AuthorizationResult.UserWithSuchLoginDoesNotExist;
            }

            var passwordSalt = userData.PasswordSalt;
            var passwordHash = _authenticatioCryptographer.ComputeHash(passwordSalt, password);
            var authorizationResult = await _medicalService.AuthorizeUserAsync(login, passwordSalt, passwordHash);
            if (authorizationResult == AuthorizationResult.Success)
            {
                SetUserFrom(userDoctorData);
            }

            return authorizationResult;
        }

        public Task SignOutAsync()
        {
            _userSettingsService.ClearUserSettings();
            SignedOut.RaiseEvent(this);

            return Task.FromResult<object>(null);
        }

        public async Task<RegistrationResult> SignUpAsync(string login, string password, string firstName, string lastName, string patronymic, string qualification)
        {
            var passwordSalt = _authenticatioCryptographer.GeneratePasswordSalt();
            var passwordHash = _authenticatioCryptographer.ComputeHash(passwordSalt, password);
            var registrationResult = await _medicalService.RegisterUserAsync(login, passwordSalt, passwordHash, firstName, lastName, patronymic, qualification);
            if (registrationResult == RegistrationResult.Success)
            {
                var userData = await _medicalService.GetUserDoctorDataAsync(login);
                SetUserFrom(userData);
            }

            return registrationResult;
        }

        private void SetUserFrom(UserDoctorData userDoctorData)
        {
            var user = new User(userDoctorData.Login, userDoctorData.FirstName, userDoctorData.LastName, userDoctorData.Patronymic);
            _userSettingsService.SetNewUser(user);
        }
    }
}
