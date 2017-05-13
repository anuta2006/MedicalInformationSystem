using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Foundation.MediaData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Controllers
{
    public class StudentController : IStudentController
    {
        private readonly IAccountService _accountService;

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public string ClassLetter { get; set; }

        public int ClassNumber { get; set; }

        public string FirstName { get; set; }

        public int Height { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public int Weight { get; set; }

        public StudentController(
            IAccountService accountService, 
            string address, 
            DateTime birthDate,
            string classLetter, 
            int classNumber,
            string firstName,
            string lastName, 
            string patronymic,
            int height,
            int weight)
        {
            _accountService = accountService;
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

        public Task<IReadOnlyCollection<IVaccinationController>> GetVaccinationAsync()
        {
            var studentInfo = new StudentInfo(FirstName, LastName, Patronymic);

            return _accountService.GetVaccinationForStudentAsync(studentInfo);
        }

        public Task<IReadOnlyCollection<IDiseaseGroupController>> GetDiseaseGroupAsync()
        {
            var studentInfo = new StudentInfo(FirstName, LastName, Patronymic);

            return _accountService.GetDiseaseGroupForStudentAsync(studentInfo);
        }
    }
}
