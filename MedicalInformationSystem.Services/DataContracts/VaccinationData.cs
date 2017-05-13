using MedicalInformationSystem.Common;
using System;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class VaccinationData
    {
        [Column("Date")]
        public DateTime Date { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
