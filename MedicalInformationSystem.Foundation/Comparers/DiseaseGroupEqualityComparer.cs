using MedicalInformationSystem.Common;
using MedicalInformationSystem.Services.DataContracts;
using System.Collections.Generic;

namespace MedicalInformationSystem.Foundation.Comparers
{
    [UsedImplicitly]
    public class DiseaseGroupEqualityComparer : IEqualityComparer<DiseaseGroupData>
    {
        public bool Equals(DiseaseGroupData x, DiseaseGroupData y)
            => x.Name.Equals(y.Name);

        public int GetHashCode(DiseaseGroupData obj)
            => obj.GetHashCode();
    }
}
