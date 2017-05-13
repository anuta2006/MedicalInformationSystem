using System;

namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IVaccinationController
    {
        DateTime Date { get; }

        string Name { get; }
    }
}
