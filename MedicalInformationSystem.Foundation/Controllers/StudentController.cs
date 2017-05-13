using MedicalInformationSystem.Foundation.Interfaces;
using System;

namespace MedicalInformationSystem.Foundation.Controllers
{
    public class StudentController : IStudentController
    {
        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public string ClassLetter { get; set; }

        public int ClassNumber { get; set; }

        public string FirstName { get; set; }

        public int Height { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public int Weight { get; set; }

        public StudentController(string address, DateTime birthDate, string classLetter, int classNumber, string firstName, string lastName, string patronymic, int height, int weight)
        {
            Address = address;
            BirthDate = birthDate.Date;
            ClassNumber = classNumber;
            ClassLetter = classLetter;
            FirstName = firstName;
            Height = height;
            LastName = lastName;
            Patronymic = patronymic;
            Weight = weight;
        }
    }
}
