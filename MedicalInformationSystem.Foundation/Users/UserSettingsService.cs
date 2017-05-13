using MedicalInformationSystem.Common.Extensions;
using MedicalInformationSystem.Foundation.Interfaces;
using System;
using static MedicalInformationSystem.Foundation.Users.User;

namespace MedicalInformationSystem.Foundation.Users
{
    public class UserSettingsService : IUserSettingsService
    {
        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            private set
            {
                if (_currentUser.Login != value.Login)
                {
                    _currentUser = value;
                    UserChanged.RaiseEvent(this);
                }
            }
        }

        public event EventHandler UserChanged;

        public UserSettingsService()
        {
            _currentUser = new NullUser();
        }

        public void ClearUserSettings()
        {
            CurrentUser = new NullUser();
        }

        public void SetNewUser(User user)
        {
            CurrentUser = user;
        }
    }
}
