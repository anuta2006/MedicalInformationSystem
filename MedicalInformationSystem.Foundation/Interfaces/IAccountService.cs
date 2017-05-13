using MedicalInformationSystem.Foundation.MediaData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IAccountService
    {
        Task<IReadOnlyCollection<IClassController>> GetAllClassesAsync();

        Task<IReadOnlyCollection<IStudentController>> GetStudentsByClassAsync(ClassInfo classInfo);

        Task<IReadOnlyCollection<IVaccinationController>> GetVaccinationForStudentAsync(StudentInfo studentInfo);

        Task<bool> DeleteStudentAsync(StudentInfo studentInfo);

        Task<bool> TransferToNextYearAsync(StudentTransferInfo transferInfo);

        Task<IReadOnlyCollection<IDiseaseGroupController>> GetDiseaseGroupForStudentAsync(StudentInfo studentInfo);
    }
}
