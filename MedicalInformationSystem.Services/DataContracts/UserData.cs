using MedicalInformationSystem.Common;

namespace MedicalInformationSystem.Services.DataContracts
{
    [UsedImplicitly]
    public class UserData
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Login")]
        public string Login { get; set; }

        [Column("PasswordSalt")]
        public string PasswordSalt { get; set; }

        [Column("PasswordHash")]
        public string PasswordHash { get; set; }
    }
}
