using MedicalInformationSystem.Foundation.Interfaces;

namespace MedicalInformationSystem.Foundation.Controllers
{
    public class DiseaseGroupController : IDiseaseGroupController
    {
        public string Name { get; set; }

        public DiseaseGroupController(string name)
        { 
            Name = name;
        }
    }
}
