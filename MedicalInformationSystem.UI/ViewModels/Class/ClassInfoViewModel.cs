using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.Student;
using Microsoft.Practices.Prism.Mvvm;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI.ViewModels.Class
{
    public class ClassInfoViewModel : BindableBase
    {
        private readonly IClassController _classController;
        private readonly IAccountService _accountService;

        private readonly IControllerViewModelProvider<IStudentController, StudentViewModel> _studentControllerViewModelProvider;

        public string Letter => _classController.Letter;

        public int Number => _classController.Number;

        public ClassInfoViewModel(
           IClassController classController,
           IAccountService accountService,
           IControllerViewModelProvider<IStudentController, StudentViewModel> provider)
        {
            _classController = classController;
            _accountService = accountService;
            _studentControllerViewModelProvider = provider;
        }

        public async Task<IReadOnlyCollection<StudentViewModel>> GetStudentsAsync()
        {
            var students = await _classController.GetStudentsAsync();

            return students.Select(_studentControllerViewModelProvider.GetViewModelFor).ToList();
        }
    }
}
