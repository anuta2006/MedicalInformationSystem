using MedicalInformationSystem.Common;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class DiseaseGroupData
    {
        [Column("Name")]
        public string Name { get; set; }
    }
}
