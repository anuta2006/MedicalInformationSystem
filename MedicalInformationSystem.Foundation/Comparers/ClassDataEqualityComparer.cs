using MedicalInformationSystem.Common;
using MedicalInformationSystem.Services.DataContracts;
using System.Collections.Generic;

namespace MedicalInformationSystem.Foundation.Comparers
{
    [UsedImplicitly]
    public class ClassDataEqualityComparer : IEqualityComparer<ClassData>
    {
        public bool Equals(ClassData x, ClassData y)
            => x.Letter.Equals(y.Letter) && x.Number.Equals(y.Number);

        public int GetHashCode(ClassData classData)
            => classData.Letter.GetHashCode() ^ classData.Number.GetHashCode();
    }
}
