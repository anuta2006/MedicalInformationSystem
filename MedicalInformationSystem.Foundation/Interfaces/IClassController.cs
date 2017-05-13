using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IClassController
    {
        string Letter { get; }

        int Number { get; }

        Task<IReadOnlyCollection<IStudentController>> GetStudentsAsync();
    }
}
