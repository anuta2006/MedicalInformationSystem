using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Foundation.MediaData;

namespace MedicalInformationSystem.Foundation.Controllers
{
    public class ClassController : IClassController
    {
        private readonly IAccountService _accountService;

        public string Letter { get; }

        public int Number { get; }

        public ClassController(
            IAccountService accountService,
            string letter, 
            int number)
        {
            _accountService = accountService;
            Letter = letter;
            Number = number;
        }

        public Task<IReadOnlyCollection<IStudentController>> GetStudentsAsync()
        {
            var classInfo = new ClassInfo(Letter, Number);

            return _accountService.GetStudentsByClassAsync(classInfo);
        }
    }
}
