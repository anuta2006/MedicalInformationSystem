using MedicalInformationSystem.Foundation.Interfaces;
using System;

namespace MedicalInformationSystem.Foundation.Controllers
{
    public class VaccinationController : IVaccinationController
    {
        public DateTime Date { get; }

        public string Name { get; }

        public VaccinationController(DateTime date, string name)
        {
            Date = date;
            Name = name;
        }
    }
}
