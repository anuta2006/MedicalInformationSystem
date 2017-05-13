using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.DiseaseGroup;
using MedicalInformationSystem.UI.ViewModels.Navigation;
using MedicalInformationSystem.UI.ViewModels.Student;
using MedicalInformationSystem.UI.ViewModels.Vaccination;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MedicalInformationSystem.UI.ViewModels.StudentEdit
{
    [UsedImplicitly]
    public class StudentEditViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private StudentViewModel _student;
        private ObservableCollection<VaccinationViewModel> _vaccinations;
        private ObservableCollection<DiseaseGroupViewModel> _diseaseGroup;

        private readonly IControllerViewModelProvider<IVaccinationController, VaccinationViewModel> _vaccinationControllerViewModelProvider;

        public IEnumerable<VaccinationViewModel> Vaccinations => _vaccinations;
        public IEnumerable<DiseaseGroupViewModel> DiseaseGroup => _diseaseGroup;

        public StudentEditViewModel(IControllerViewModelProvider<IVaccinationController, VaccinationViewModel> provider)
        {
            _vaccinationControllerViewModelProvider = provider;
            _vaccinations = new ObservableCollection<VaccinationViewModel>();
            _diseaseGroup = new ObservableCollection<DiseaseGroupViewModel>();
        }

        public StudentViewModel Student
        {
            get
            {
                return _student;
            }
            private set
            {
                SetProperty(ref _student, value);
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
            => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;

            Student = (StudentViewModel)navigationContext.Parameters[NavigationParameterKeys.Student];
            if (Student != null)
            {
                var vaccinations = await Student.GetVaccinationAsync();
                var sortedVaccinations = vaccinations.OrderByDescending(x => x.Date).ToList();
                _vaccinations.Clear();
                _vaccinations.AddRange(sortedVaccinations);

                var diseaseGroup = await Student.GetDiseaseGroupAsync();
                _diseaseGroup.Clear();
                _diseaseGroup.AddRange(diseaseGroup);
            }
        }
    }
}
