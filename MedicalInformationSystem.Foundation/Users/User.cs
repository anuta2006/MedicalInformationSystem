using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Users
{
    public class User
    {
        public string Login { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public string Patronymic { get; }

        public User(string login, string firstName, string lastName, string patronymic)
        {
            Login = login;
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
        }


        public class NullUser : User
        {
            public NullUser()
                : base(String.Empty, String.Empty, String.Empty, String.Empty)
            {

            }
        }
    }
}
