using MedicalInformationSystem.Foundation.Interfaces;
using Microsoft.Practices.Prism.Mvvm;

namespace MedicalInformationSystem.UI.ViewModels.DiseaseGroup
{
    public class DiseaseGroupViewModel : BindableBase
    {
        private readonly IDiseaseGroupController _diseaseController;

        public string Name => _diseaseController.Name;

        public DiseaseGroupViewModel(IDiseaseGroupController diseaseController)
        {
            _diseaseController = diseaseController;
        }
    }
}
