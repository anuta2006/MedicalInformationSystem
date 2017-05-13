namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IControllerViewModelFactory<in TController, out TViewModel>
    {
        TViewModel CreateFrom(TController labelController);
    }
}
