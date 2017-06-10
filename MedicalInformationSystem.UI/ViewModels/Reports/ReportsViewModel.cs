using MedicalInformationSystem.Foundation.Reports.Word;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MedicalInformationSystem.UI.ViewModels.Reports
{
    public class ReportsViewModel : BindableBase
    {
        private ObservableCollection<ReportViewModel> _reportsByPeriod;

        private ObservableCollection<ReportViewModel> _receptionsByClass;

        private ObservableCollection<ReportViewModel> _reportsByDesiase;

        public ICommand SaveAsDocCommand { get; private set; }

        public IEnumerable<ReportViewModel> ReportsByPeriod => _reportsByPeriod;

        public IEnumerable<ReportViewModel> ReceptionsByClass => _receptionsByClass;

        public IEnumerable<ReportViewModel> ReportsByDesiase => _reportsByDesiase;

        public ReportsViewModel()
        {
            SaveAsDocCommand = new DelegateCommand(CreateWordDocument);

            _reportsByPeriod = new ObservableCollection<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    Class = "5A",
                    Date = "20.05.2017",
                    Diagnosis = "ОРВИ",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "температура тела 37,8"
                },
                new ReportViewModel()
                {
                    Class = "2А",
                    Date = "18.05.2017",
                    Diagnosis = "Растяжение связок",
                    FirstName = "Илья",
                    LastName = "Сидоров",
                    Patronymic = "Николаевич",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в руке"
                },
                new ReportViewModel()
                {
                    Class = "1A",
                    Date = "18.05.2017",
                    Diagnosis = "Пищевое отравление",
                    FirstName = "Екатерина",
                    LastName = "Драенкова",
                    Patronymic = "Витальевна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "тошнота, температура тела 37,8"
                },
                new ReportViewModel()
                {
                    Class = "9Б",
                    Date = "17.05.2017",
                    Diagnosis = "Язвенная болезнь",
                    FirstName = "Анастасия",
                    LastName = "Хацкевич",
                    Patronymic = "Леонидовна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "болезнь в желудке"
                },
                new ReportViewModel()
                {
                    Class = "4В",
                    Date = "17.05.2017",
                    Diagnosis = "Ушиб",
                    FirstName = "Марина",
                    LastName = "Селиванов",
                    Patronymic = "Вадимовна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "повреждена нога"
                },
                new ReportViewModel()
                {
                    Class = "10A",
                    Date = "15.05.2017",
                    Diagnosis = "Отит",
                    FirstName = "Светлана",
                    LastName = "Мороз",
                    Patronymic = "Евгеньевна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в ухе"
                },
            };

            _receptionsByClass = new ObservableCollection<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    Date = "23.05.2017",
                    Diagnosis = "ОРВИ",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "температура тела 37,8"
                },
                new ReportViewModel()
                {
                    Date = "18.05.2017",
                    Diagnosis = "Растяжение связок",
                    FirstName = "Илья",
                    LastName = "Сидоров",
                    Patronymic = "Николаевич",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в руке"
                },
                new ReportViewModel()
                {
                    Date = "06.05.2017",
                    Diagnosis = "Пищевое отравление",
                    FirstName = "Екатерина",
                    LastName = "Драенкова",
                    Patronymic = "Витальевна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "тошнота, температура тела 37,8"
                },
                new ReportViewModel()
                {
                    Date = "30.04.2017",
                    Diagnosis = "Язвенная болезнь",
                    FirstName = "Анастасия",
                    LastName = "Хацкевич",
                    Patronymic = "Леонидовна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "болезнь в желудке"
                },
                new ReportViewModel()
                {
                    Date = "27.04.2017",
                    Diagnosis = "Ушиб",
                    FirstName = "Марина",
                    LastName = "Селиванов",
                    Patronymic = "Вадимовна",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "повреждена нога"
                },
                new ReportViewModel()
                {
                    Date = "20.04.2017",
                    Diagnosis = "Отит",
                    FirstName = "Вадим",
                    LastName = "Хомченовский",
                    Patronymic = "Владимирович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в ухе"
                },
                new ReportViewModel()
                {
                    Date = "05.04.2017",
                    Diagnosis = "Ушиб",
                    FirstName = "Сергей",
                    LastName = "Кислый",
                    Patronymic = "Петрович",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "повреждена нога"
                },
                new ReportViewModel()
                {
                    Date = "01.04.2017",
                    Diagnosis = "Отит",
                    FirstName = "Михаил",
                    LastName = "Миронченко",
                    Patronymic = "Андреевич",
                    Notes = "",
                    Purpose = "",
                    Symptoms = "боли в ухе"
                },
            };

            _reportsByDesiase = new ObservableCollection<ReportViewModel>()
            {
                new ReportViewModel()
                {
                    BirthDate = "13.05.2005",
                    Class = "5A",
                    FirstName = "Александр",
                    LastName = "Жук",
                    Patronymic = "Владимирович"
                },
                new ReportViewModel()
                {
                    BirthDate = "13.05.2002",
                    Class = "7В",
                    FirstName = "Александр",
                    LastName = "Сидоров",
                    Patronymic = "Петрович"
                },
                new ReportViewModel()
                {
                    BirthDate = "23.10.2000",
                    Class = "10A",
                    FirstName = "Константин",
                    LastName = "Герасимович",
                    Patronymic = "Сергеевич"
                }
            };
        }

        private void CreateWordDocument()
        {
            WordDocument.CreateDocument();
        }
    }
}
