using MedicalInformationSystem.Common;
using MedicalInformationSystem.Foundation.Interfaces;
using System.Collections.Concurrent;
using System.Linq;

namespace MedicalInformationSystem.Foundation.Providers
{
    [UsedImplicitly]
    public class CachingControllerViewModelProvider<TController, TViewModel> : IControllerViewModelProvider<TController, TViewModel>
    {
        private readonly IControllerViewModelFactory<TController, TViewModel> _controllerViewModelFactory;

        private readonly ConcurrentDictionary<TController, TViewModel> _controllersViewModelsCash;

        public CachingControllerViewModelProvider(
            IControllerViewModelFactory<TController, TViewModel> controllerViewModelFactory,
            IAuthenticationService authenticationService)
        {
            _controllerViewModelFactory = controllerViewModelFactory;

            _controllersViewModelsCash = new ConcurrentDictionary<TController, TViewModel>();

            authenticationService.SignedOut += AuthenticationServiceOnSignedOut;
        }

        public TViewModel GetViewModelFor(TController controller)
        {
            TViewModel viewModel;
            if (!_controllersViewModelsCash.TryGetValue(controller, out viewModel))
            {
                viewModel = _controllerViewModelFactory.CreateFrom(controller);
                if (!_controllersViewModelsCash.TryAdd(controller, viewModel))
                {
                    return _controllersViewModelsCash[controller];
                }
            }

            return viewModel;
        }

        public TController GetControllerOf(TViewModel viewModel)
            => _controllersViewModelsCash.Single(pair => pair.Value.Equals(viewModel)).Key;


        private void AuthenticationServiceOnSignedOut(object sender, System.EventArgs e)
        {
            ClearCache();
        }

        private void ClearCache()
        {
            _controllersViewModelsCash.Clear();
        }
    }
}
