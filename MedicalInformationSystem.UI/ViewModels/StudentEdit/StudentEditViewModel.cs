using MedicalInformationSystem.Common;
using MedicalInformationSystem.UI.ViewModels.Navigation;
using MedicalInformationSystem.UI.ViewModels.Student;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;

namespace MedicalInformationSystem.UI.ViewModels.StudentEdit
{
    [UsedImplicitly]
    public class StudentEditViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private StudentViewModel _student;

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

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;

            Student = (StudentViewModel)navigationContext.Parameters[NavigationParameterKeys.Student];
        }
    }
}
