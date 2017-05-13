using MedicalInformationSystem.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class UserDoctorData
    {
        [Column("Login")]
        public string Login { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Patronymic")]
        public string Patronymic { get; set; }

        [Column("Qualification")]
        public string Qualification { get; set; }
    }
}
