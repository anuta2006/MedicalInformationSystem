﻿using MedicalInformationSystem.Foundation.Interfaces;
using Microsoft.Practices.Prism.Mvvm;
using System;

namespace MedicalInformationSystem.UI.ViewModels.Vaccination
{
    public class VaccinationViewModel : BindableBase
    {
        private readonly IVaccinationController _vaccinationController;
        private readonly IAccountService _accountService;

        public string Name => _vaccinationController.Name;

        public string Date => _vaccinationController.Date.Date.ToString("dd/MM/yyyy");

        public VaccinationViewModel(IVaccinationController vacController, IAccountService accountService)
        {
            _accountService = accountService;
            _vaccinationController = vacController;
        }
    }
}
