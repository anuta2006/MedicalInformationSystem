using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Foundation.MediaData;
using MedicalInformationSystem.UI.Navigation;
using MedicalInformationSystem.UI.ViewModels.Class;
using MedicalInformationSystem.UI.ViewModels.Navigation;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MedicalInformationSystem.UI.ViewModels.Student
{
    [UsedImplicitly]
    public class StudentsViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private readonly IAccountService _accountService;
        private readonly IControllerViewModelProvider<IStudentController, StudentViewModel> _studentControllerViewModelProvider;
        private ClassInfoViewModel _class;
        private ObservableCollection<StudentViewModel> _students;

        public ClassInfoViewModel Class
        {
            get
            {
                return _class;
            }
            private set
            {
                SetProperty(ref _class, value);
            }
        }

        public IEnumerable<StudentViewModel> Students => _students;

        public ICommand DeleteCommand { get; private set; }

        public ICommand TransferNextYearCommand { get; private set; }

        public ICommand EditStudentCommand { get; private set; }

        public ICommand ReceptionCommand { get; private set; }

        public StudentsViewModel(
             IAccountService accountService,
             IControllerViewModelProvider<IStudentController, StudentViewModel> provider)
        {
            _accountService = accountService;
            _studentControllerViewModelProvider = provider;

            EditStudentCommand = new DelegateCommand<object>(EditStudent);
            DeleteCommand = new DelegateCommand<object>(Delete);
            TransferNextYearCommand = new DelegateCommand<object>(TransferNextYear);
            ReceptionCommand = new DelegateCommand<object>(Reception);

            _students = new ObservableCollection<StudentViewModel>();
        }

        private async void Delete(object obj)
        {
            if (obj is StudentViewModel)
            {
                var student = obj as StudentViewModel;
                var studentInfo = new StudentInfo(student.FirstName, student.LastName, student.Patronymic);
                await _accountService.DeleteStudentAsync(studentInfo);

                _students.Remove(student);
            }
        }

        private async void TransferNextYear(object obj)
        {
            if (obj is StudentViewModel)
            {
                var student = obj as StudentViewModel;
                var transferInfo = new StudentTransferInfo(student.FirstName, student.LastName, student.Patronymic, Class.Letter, Class.Number);
                await _accountService.TransferToNextYearAsync(transferInfo);

                _students.Remove(student);
            }
        }

        private void Reception(object obj)
        {
            var student = obj as StudentViewModel;
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.StudentRegion,
                        MedicalInformationSystemViews.StudentReceptionView,
                        new Dictionary<string, object> { [NavigationParameterKeys.Student] = student });
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
                    => true;

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public async void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;

            Class = (ClassInfoViewModel)navigationContext.Parameters[NavigationParameterKeys.Class];
            if (Class != null)
            {
                var students = await Class.GetStudentsAsync();
                _students.Clear();
                _students.AddRange(students);
            }
        }

        private void EditStudent(object addedItems)
        {
            var student = addedItems as StudentViewModel;
            _regionNavigationService.Navigate(MedicalInformationSystemRegions.StudentRegion,
                        MedicalInformationSystemViews.StudentEditView,
                        new Dictionary<string, object> { [NavigationParameterKeys.Student] = student });
        }
    }
}
