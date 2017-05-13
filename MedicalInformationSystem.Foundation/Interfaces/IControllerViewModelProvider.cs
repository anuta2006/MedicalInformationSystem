namespace MedicalInformationSystem.Foundation.Interfaces
{
    public interface IControllerViewModelProvider<TController, TViewModel>
    {
        TViewModel GetViewModelFor(TController controller);

        TController GetControllerOf(TViewModel viewModel);
    }
}
