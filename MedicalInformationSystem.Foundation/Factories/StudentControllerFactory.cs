using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Controllers;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services.DataContracts;

namespace MedicalInformationSystem.Foundation.Factories
{
    [UsedImplicitly]
    public class StudentControllerFactory : IEntityControllerFactory<StudentData, IStudentController>
    {
        public IStudentController CreateFrom(StudentData entity)
            => new StudentController(entity.Address,
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
