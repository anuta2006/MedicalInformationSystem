using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Controllers;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services.DataContracts;

namespace MedicalInformationSystem.Foundation.Factories
{
    [UsedImplicitly]
    public class DiseaseGroupControllerFactory : IEntityControllerFactory<DiseaseGroupData, IDiseaseGroupController>
    {
        public IDiseaseGroupController CreateFrom(DiseaseGroupData entity)
            => new DiseaseGroupController(entity.Name);
    }
}
