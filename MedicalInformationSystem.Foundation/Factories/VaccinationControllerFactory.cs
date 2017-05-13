using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Controllers;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services.DataContracts;

namespace MedicalInformationSystem.Foundation.Factories
{
    [UsedImplicitly]
    public class VaccinationControllerFactory : IEntityControllerFactory<VaccinationData, IVaccinationController>
    {
        public IVaccinationController CreateFrom(VaccinationData entity)
            => new VaccinationController(entity.Date, entity.Name);
    }
}
