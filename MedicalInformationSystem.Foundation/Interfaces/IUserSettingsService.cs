using MedicalInformationSystem.Foundation.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IUserSettingsService
    {
        User CurrentUser { get; }

        event EventHandler UserChanged;

        void SetNewUser(User user);

        void ClearUserSettings();
    }
}
