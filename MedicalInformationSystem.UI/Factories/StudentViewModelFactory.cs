using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.Student;

namespace MedicalInformationSystem.UI.Factories
{
    [UsedImplicitly]
    public class StudentViewModelFactory : IControllerViewModelFactory<IStudentController, StudentViewModel>
    {
        private readonly IAccountService _accountService;

        public StudentViewModelFactory(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public StudentViewModel CreateFrom(IStudentController studentController)
            => new StudentViewModel(studentController, _accountService);
    }
}
