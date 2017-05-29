using MedicalInformationSystem.Common;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class QualificationData
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
