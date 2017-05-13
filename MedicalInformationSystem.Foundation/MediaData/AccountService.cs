using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Foundation.Users;
using MedicalInformationSystem.Services.DataContracts;
using MedicalInformationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Foundation.MediaData
{
    [UsedImplicitly]
    public class AccountService : IAccountService
    {
        private readonly IMedicalInformationService _medicalService;
        private readonly IUserSettingsService _userSettingsService;
        private readonly IEntityControllerProvider<ClassData, IClassController> _classControllerProvider;
        private readonly IEntityControllerProvider<StudentData, IStudentController> _studentControllerProvider;
        private readonly IEntityControllerProvider<VaccinationData, IVaccinationController> _vaccinationControllerProvider;
        private readonly IEntityControllerProvider<DiseaseGroupData, IDiseaseGroupController> _diseaseGroupControllerProvider;

        private User _targetUser;

        public AccountService(
            IMedicalInformationService medicalService,
            IUserSettingsService userSettingsService,
            IEntityControllerProvider<ClassData, IClassController> classControllerProvider,
            IEntityControllerProvider<StudentData, IStudentController> studentControllerProvider,
            IEntityControllerProvider<VaccinationData, IVaccinationController> vaccinationControllerProvider,
            IEntityControllerProvider<DiseaseGroupData, IDiseaseGroupController> diseaseGroupControllerProvider)
        {
            _medicalService = medicalService;
            _userSettingsService = userSettingsService;
            _classControllerProvider = classControllerProvider;
            _studentControllerProvider = studentControllerProvider;
            _vaccinationControllerProvider = vaccinationControllerProvider;
            _diseaseGroupControllerProvider = diseaseGroupControllerProvider;
        }


        public async Task<IReadOnlyCollection<IClassController>> GetAllClassesAsync()
        {
            var userLogin = GetCurrentUserLogin();
            var rawClasses = await _medicalService.GetAllClassesAsync();

            return rawClasses.Select(_classControllerProvider.GetControllerFor).ToList();
        }

        public async Task<IReadOnlyCollection<IStudentController>> GetStudentsByClassAsync(ClassInfo classInfo)
        {
            var rawStudents = await _medicalService.GetStudentsByClassAsync(classInfo.Letter, classInfo.Number);

            return rawStudents.Select(_studentControllerProvider.GetControllerFor).ToList();
        }

        public async Task<bool> DeleteStudentAsync(StudentInfo studentInfo)
        {
            var currentUserLogin = GetCurrentUserLogin();
            var firstName = studentInfo.FirstName;
            var lastName = studentInfo.LastName;
            var patronymic = studentInfo.Patronymic;

            return await _medicalService.DeleteStudentAsync(firstName, lastName, patronymic);
        }

        public async Task<bool> TransferToNextYearAsync(StudentTransferInfo transferInfo)
        {
            var firstName = transferInfo.FirstName;
            var lastName = transferInfo.LastName;
            var patronymic = transferInfo.Patronymic;
            var classLetter = transferInfo.ClassLetter;
            var classNumber = transferInfo.ClassNumber;

            return await _medicalService.TransferToNextYearAsync(firstName, lastName, patronymic, classLetter, classNumber);
        }

        public async Task<IReadOnlyCollection<IVaccinationController>> GetVaccinationForStudentAsync(StudentInfo studentInfo)
        {
            var firstName = studentInfo.FirstName;
            var lastName = studentInfo.LastName;
            var patronymic = studentInfo.Patronymic;

            var rawVaccination = await _medicalService.GetVaccinationForStudentAsync(firstName, lastName, patronymic);

            return rawVaccination.Select(_vaccinationControllerProvider.GetControllerFor).ToList();
        }

        public async Task<IReadOnlyCollection<IDiseaseGroupController>> GetDiseaseGroupForStudentAsync(StudentInfo studentInfo)
        {
            var firstName = studentInfo.FirstName;
            var lastName = studentInfo.LastName;
            var patronymic = studentInfo.Patronymic;

            var rawDiseaseGroup = await _medicalService.GetDiseaseGroupForStudentAsync(firstName, lastName, patronymic);

            return rawDiseaseGroup.Select(_diseaseGroupControllerProvider.GetControllerFor).ToList();
        }

        private string GetCurrentUserLogin()
            => _userSettingsService.CurrentUser.Login;
    }
}
