using MedicalInformationSystem.Common;
using MedicalInformationSystem.Services.DataContracts;
using System.Collections.Generic;

namespace MedicalInformationSystem.Foundation.Comparers
{
    [UsedImplicitly]
    public class StudentDataEqualityComparer : IEqualityComparer<StudentData>
    {
        public bool Equals(StudentData x, StudentData y)
            => x.FirstName.Equals(y.FirstName) && x.LastName.Equals(y.LastName)
                && x.Patronymic.Equals(y.Patronymic);

        public int GetHashCode(StudentData student)
            => student.FirstName.GetHashCode()
                ^ student.LastName.GetHashCode() ^ student.Patronymic.GetHashCode();
    }
}
