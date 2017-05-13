using MedicalInformationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IAuthenticationService
    {
        event EventHandler SignedOut;

        Task<RegistrationResult> SignUpAsync(string login, string password, string firstName, string lastName, string patronymic, string qualification);

        Task<AuthorizationResult> SignInAsync(string login, string password);

        Task SignOutAsync();
    }
}
