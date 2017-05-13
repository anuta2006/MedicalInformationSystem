using MedicalInformationSystem.Common;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class ClassData
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Number")]
        public int Number { get; set; }

        [Column("Letter")]
        public string Letter { get; set; }
    }
}
