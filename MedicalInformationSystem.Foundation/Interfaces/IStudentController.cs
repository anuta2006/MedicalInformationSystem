using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IStudentController
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Patronymic { get; set; }

        DateTime BirthDate { get; set; }

        string Address { get; set; }

        int Height { get; set; }

        int Weight { get; set; }

        string ClassLetter { get; set; }

        int ClassNumber { get; set; }

        Task<IReadOnlyCollection<IVaccinationController>> GetVaccinationAsync();

        Task<IReadOnlyCollection<IDiseaseGroupController>> GetDiseaseGroupAsync();
    }
}
