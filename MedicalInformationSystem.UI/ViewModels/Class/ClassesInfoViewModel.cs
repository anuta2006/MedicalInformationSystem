using MedicalInformationSystem.Common;
using MedicalInformationSystem.Common.Extensions;
using MedicalInformationSystem.Foundation.Interfaces;
using MedicalInformationSystem.UI.Navigation;
using MedicalInformationSystem.UI.ViewModels.Navigation;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Regions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace MedicalInformationSystem.UI.ViewModels.Class
{
    [UsedImplicitly]
    public class ClassesInfoViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private readonly IAccountService _accountService;
        private readonly IControllerViewModelProvider<IClassController, ClassInfoViewModel> _classControllerViewModelProvider;


        private ClassInfoViewModel _classSelectedItem;
        public ClassInfoViewModel ClassSelectedItem
        {
            get { return _classSelectedItem; }
            set
            {
                SetProperty(ref _classSelectedItem, value);
                if (ClassSelectedItem != null)
                {
                    _regionNavigationService.Navigate(MedicalInformationSystemRegions.StudentRegion,
                        MedicalInformationSystemViews.StudentsView,
                        new Dictionary<string, object> { [NavigationParameterKeys.Class] = ClassSelectedItem });
                }
            }
        }

        private readonly ObservableCollection<ClassInfoViewModel> _classes;

        public IEnumerable<ClassInfoViewModel> Classes => _classes;

        public ICommand UpdateClassesListCommand { get; private set; }

        public ClassesInfoViewModel(
            IAccountService accountService,
            IControllerViewModelProvider<IClassController, ClassInfoViewModel> provider)
        {
            _accountService = accountService;
            _classControllerViewModelProvider = provider;

            UpdateClassesListCommand = new DelegateCommand(UpdateClassesList);

            _classes = new ObservableCollection<ClassInfoViewModel>();
        }

        async void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;
            GetClassesList();
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
            => true;

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        private void UpdateClassesList()
        {
            GetClassesList();
        }

        private async void GetClassesList()
        {
            var classes = await _accountService.GetAllClassesAsync();
            var sortdedClasses = classes.OrderBy(x => x.Number).ThenBy(x => x.Letter).ToList();
            _classes.Clear();
            sortdedClasses.Select(_classControllerViewModelProvider.GetViewModelFor).ForEach(_classes.Add);
        }
    }
}
