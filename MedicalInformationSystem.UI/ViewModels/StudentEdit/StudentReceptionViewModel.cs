using MedicalInformationSystem.Common;
using MedicalInformationSystem.UI.Navigation;
using MedicalInformationSystem.UI.ViewModels.Navigation;
using MedicalInformationSystem.UI.ViewModels.Student;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MedicalInformationSystem.UI.ViewModels.StudentEdit
{
    [UsedImplicitly]
    public class StudentReceptionViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private StudentViewModel _student;

        public ICommand GoBackCommand { get; }

        public ICommand NewReceptionCommand { get; private set; }

        public StudentReceptionViewModel()
        {
            GoBackCommand = new DelegateCommand(GoBack);
            NewReceptionCommand = new DelegateCommand<object>(NewReception);
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
        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;

            Student = (StudentViewModel)navigationContext.Parameters[NavigationParameterKeys.Student];
            if (Student != null)
            {
            }
        }

        private void GoBack()
        {
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.StudentRegion, MedicalInformationSystemViews.StudentsView);
        }

        private void NewReception(object obj)
        {
            var x = "sdfg";

        }
    }
}
