using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI.ViewModels.Reports
{
    public class ReportsViewModel : BindableBase
    {
        private ObservableCollection<ReportViewModel> _reportsByPeriod;

        private ObservableCollection<ReportViewModel> _receptionsByClass;

        private ObservableCollection<ReportViewModel> _reportsByDesiase;

        public IEnumerable<ReportViewModel> ReportsByPeriod => _reportsByPeriod;

        public IEnumerable<ReportViewModel> ReceptionsByClass => _receptionsByClass;

        public IEnumerable<ReportViewModel> ReportsByDesiase => _reportsByDesiase;

        public ReportsViewModel()
        {
            _reportsByPeriod = new ObservableCollection<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    Class = "5A",
                    Date = "13.05.2017",
                    Diagnosis = "ОРВИ",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "температура тела 37,8"
                }
            };

            _receptionsByClass = new ObservableCollection<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    Date = "13.05.2017",
                    Diagnosis = "ОРВИ",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "температура тела 37,8"
                }
            };

            _reportsByDesiase = new ObservableCollection<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    BirthDate = "13.05.1995",
                    Class = "5A",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович"
                }
            };
        }
    }
}
