using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Controllers;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services.DataContracts;
using System;

namespace MedicalInformationSystem.Foundation.Factories
{
    [UsedImplicitly]
    public class StudentControllerFactory : IEntityControllerFactory<StudentData, IStudentController>
    {
        private readonly Lazy<IAccountService> _lazyAccountService;

        private IAccountService AccountService => _lazyAccountService.Value;

        public StudentControllerFactory(Lazy<IAccountService> lazyAccountService)
        {
            _lazyAccountService = lazyAccountService;
        }

        public IStudentController CreateFrom(StudentData entity)
            => new StudentController(
                AccountService,
                entity.Address,
                entity.BirthDate,
                entity.Letter,
                entity.Number,
                entity.FirstName,
                entity.LastName,
                entity.Patronymic,
                entity.Height, 
                entity.Weight);
    }
}
