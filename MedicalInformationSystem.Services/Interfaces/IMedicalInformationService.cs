using MedicalInformationSystem.Services.DataContracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Services.Interfaces
{
    public interface IMedicalInformationService
    {
        Task<RegistrationResult> RegisterUserAsync(string login, string passwordSalt, string passwordHash, string firstName, string lastName, string patronymic, string qualification);

        Task<AuthorizationResult> AuthorizeUserAsync(string login, string passwordSalt, string passwordHash);

        Task<UserDoctorData> GetUserDoctorDataAsync(string login);

        Task<UserData> GetUserDataAsync(string login);

        Task<IReadOnlyCollection<ClassData>> GetAllClassesAsync();

        Task<IReadOnlyCollection<StudentData>> GetStudentsByClassAsync(string classLetter, int classNumber);

        Task<bool> DeleteStudentAsync(string firstName, string lastName, string patronymic);

        Task<bool> TransferToNextYearAsync(string firstName, string lastName, string patronymic, string classLetter, int classNumber);

        Task<IReadOnlyCollection<VaccinationData>> GetVaccinationForStudentAsync(string firstName, string lastName, string patronymic);
    }
}
