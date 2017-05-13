using MedicalInformationSystem.Common;
using MedicalInformationSystem.Services.DataContracts;
using System.Collections.Generic;

namespace MedicalInformationSystem.Foundation.Comparers
{
    [UsedImplicitly]
    public class VaccinationDataEqualityComparer : IEqualityComparer<VaccinationData>
    {
        public bool Equals(VaccinationData x, VaccinationData y)
        {
            return x.Date.Equals(y.Date) && x.Name.Equals(y.Name);
        }

        public int GetHashCode(VaccinationData obj)
        {
            return obj.Date.GetHashCode() ^ obj.Name.GetHashCode();
        }
    }
}
