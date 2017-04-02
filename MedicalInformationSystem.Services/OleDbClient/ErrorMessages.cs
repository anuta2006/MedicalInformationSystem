using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Services.OleDbClient
{
    internal static class ErrorMessages
    {
        public const string UserAlreadyExists = "User with such login already exists";
        public const string NoUserWithSuchLogin = "User with such login doesn't exist";
        public const string IncorrectPassword = "Incorrect password";
    }
}
