using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.Vaccination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI.Factories
{
    [UsedImplicitly]
    public class VaccinationViewModelFactory : IControllerViewModelFactory<IVaccinationController, VaccinationViewModel>
    {
        private readonly IAccountService _accountService;

        public VaccinationViewModelFactory(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public VaccinationViewModel CreateFrom(IVaccinationController vaccinaationController)
            => new VaccinationViewModel(vaccinaationController, _accountService);
    }
}
