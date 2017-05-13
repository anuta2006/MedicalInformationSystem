using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.ViewModels.DiseaseGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI.Factories
{
    [UsedImplicitly]
    public class DiseaseGroupViewModelFactory : IControllerViewModelFactory<IDiseaseGroupController, DiseaseGroupViewModel>
    {
        public DiseaseGroupViewModel CreateFrom(IDiseaseGroupController labelController)
            => new DiseaseGroupViewModel(labelController);
    }
}
