using MedicalInformationSystem.Services.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Services.Interfaces
{
    public interface IMedicalInformationService
    {
        Task<UserData> GetUserDataAsync(string login);
    }
}
