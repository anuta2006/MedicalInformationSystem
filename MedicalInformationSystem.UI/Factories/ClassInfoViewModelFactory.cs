using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.Class;
using MedicalInformationSystem.UI.ViewModels.Student;

namespace MedicalInformationSystem.UI.Factories
{
    [UsedImplicitly]
    public class ClassInfoViewModelFactory : IControllerViewModelFactory<IClassController, ClassInfoViewModel>
    {
        private readonly IAccountService _accountService;
        private readonly IControllerViewModelProvider<IStudentController, StudentViewModel> _studentControllerViewModelProvider;


        public ClassInfoViewModelFactory(IAccountService accountService,
            IControllerViewModelProvider<IStudentController, StudentViewModel> provider)
        {
            _accountService = accountService;
            _studentControllerViewModelProvider = provider;
        }


        public ClassInfoViewModel CreateFrom(IClassController classController)
            => new ClassInfoViewModel(classController, _accountService, _studentControllerViewModelProvider);

    }
}
