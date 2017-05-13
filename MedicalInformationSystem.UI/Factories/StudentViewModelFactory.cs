using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.DiseaseGroup;
using MedicalInformationSystem.UI.ViewModels.Student;
using MedicalInformationSystem.UI.ViewModels.Vaccination;

namespace MedicalInformationSystem.UI.Factories
{
    [UsedImplicitly]
    public class StudentViewModelFactory : IControllerViewModelFactory<IStudentController, StudentViewModel>
    {
        private readonly IAccountService _accountService;
        private readonly IControllerViewModelProvider<IVaccinationController, VaccinationViewModel> _vaccinationControllerViewModelProvider;
        private readonly IControllerViewModelProvider<IDiseaseGroupController, DiseaseGroupViewModel> _diseaseGroupControllerViewModelProvider;

        public StudentViewModelFactory(IAccountService accountService,
            IControllerViewModelProvider<IVaccinationController, VaccinationViewModel> vaccinationProvider,
            IControllerViewModelProvider<IDiseaseGroupController, DiseaseGroupViewModel> diseaseGroupProvider)
        {
            _accountService = accountService;
            _vaccinationControllerViewModelProvider = vaccinationProvider;
            _diseaseGroupControllerViewModelProvider = diseaseGroupProvider;
        }

        public StudentViewModel CreateFrom(IStudentController studentController)
            => new StudentViewModel(studentController, _accountService, _vaccinationControllerViewModelProvider, _diseaseGroupControllerViewModelProvider);
    }
}
