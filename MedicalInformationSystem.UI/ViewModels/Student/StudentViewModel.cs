using MedicalInformationSystem.Foundation.Interfaces;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System;

namespace MedicalInformationSystem.UI.ViewModels.Student
{
    public class StudentViewModel : BindableBase
    {
        private readonly IStudentController _studentController;
        private readonly IAccountService _accountService;
        
        public string FirstName
        {
            get
            {
                return _studentController.FirstName;
            }
            set
            {
                _studentController.FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _studentController.LastName;
            }
            set
            {
                _studentController.LastName = value;
            }
        }

        public string Patronymic
        {
            get
            {
                return _studentController.Patronymic;
            }
            set
            {
                _studentController.Patronymic = value;
            }
        }
        public string Address
        {
            get
            {
                return _studentController.Address;
            }
            set
            {
                _studentController.Address = value;
            }
        }
        public string BirthDate
        {
            get
            {
                return _studentController.BirthDate.Date.ToString("dd/MM/yyyy");
            }
        }

        public int Height
        {
            get
            {
                return _studentController.Height;
            }
            set
            {
                _studentController.Height = value;
            }
        }

        public int Weight
        {
            get
            {
                return _studentController.Weight;
            }
            set
            {
                _studentController.Weight = value;
            }
        }

        public string ClassLetter
        {
            get
            {
                return _studentController.ClassLetter;
            }
            set
            {
                _studentController.ClassLetter = value;
            }
        }

        public int ClassNumber
        {
            get
            {
                return _studentController.ClassNumber;
            }
            set
            {
                _studentController.ClassNumber = value;
            }
        }

        public StudentViewModel(
            IStudentController studentController,
            IAccountService accountService)
        {
            _studentController = studentController;
            _accountService = accountService;
        }
    }
}
