using MedicalInformationSystem.Common;
using System;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class StudentData
    {
        //[Column("Id")]
        //public int Id { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Patronymic")]
        public string Patronymic { get; set; }

        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Height")]
        public int Height { get; set; }

        [Column("Weight")]
        public int Weight { get; set; }

        [Column("Letter")]
        public string Letter { get; set; }

        [Column("Number")]
        public int Number { get; set; }

        //[Column("ClassId")]
        //public int ClassId { get; set; }

        //[Column("DiseaseGroupId")]
        //public int DiseaseGroupId { get; set; }

        //[Column("ReceptionSymptomId")]
        //public int ReceptionSymptomId { get; set; }
    }
}
