using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Controllers;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.Services.DataContracts;
using System;

namespace MedicalInformationSystem.Foundation.Factories
{
    [UsedImplicitly]
    public class ClassControllerFactory : IEntityControllerFactory<ClassData, IClassController>
    {
        private readonly Lazy<IAccountService> _lazyAccountService;

        private IAccountService AccountService => _lazyAccountService.Value;

        public ClassControllerFactory(Lazy<IAccountService> lazyAccountService)
        {
            _lazyAccountService = lazyAccountService;
        }

        public IClassController CreateFrom(ClassData entity)
            => new ClassController(AccountService, entity.Letter, entity.Number);
    }
}
