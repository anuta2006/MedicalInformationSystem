using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.MediaData
{
    public class StudentTransferInfo
    {
        public string FirstName { get; }

        public string LastName { get; }

        public string Patronymic { get; }

        public string ClassLetter { get; }

        public int ClassNumber { get; }

        public StudentTransferInfo(string firstName, string lastName, string patronymic, string letter, int number)
        {
            FirstName = firstName;
            LastName = lastName;
            Patronymic = patronymic;
            ClassLetter = letter;
            ClassNumber = number;
        }
    }
}
