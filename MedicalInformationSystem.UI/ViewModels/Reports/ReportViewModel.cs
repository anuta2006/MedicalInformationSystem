using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI.ViewModels.Reports
{
    public class ReportViewModel : BindableBase
    {
        public string BirthDate { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Patronymic { get; set; }

        public string Date { get; set; }

        public string Class { get; set; }

        public string Symptoms { get; set; }

        public string Diagnosis { get; set; }

        public string Purpose { get; set; }

        public string Notes { get; set; }
    }
}
