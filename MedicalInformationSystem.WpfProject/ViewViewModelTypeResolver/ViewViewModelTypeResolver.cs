using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.WpfProject.ViewViewModelTypeResolver
{
    public class ViewViewModelTypeResolver : IViewViewModelTypeResolver
    {
        private static readonly IReadOnlyCollection<string> ViewSuffixes = new[] { "Page", "View", "SettingsFlyout" };
        private const string ViewModelNameSuffix = "ViewModel";

        private readonly Lazy<IDictionary<string, Type>> _uiAssemblyExportedTypes;


        private IDictionary<string, Type> UiAssemblyExportedTypes => _uiAssemblyExportedTypes.Value;


        public ViewViewModelTypeResolver(Assembly uiAssembly)
        {
            _uiAssemblyExportedTypes = new Lazy<IDictionary<string, Type>>(() => GetUiAssemblyExportedTypes(uiAssembly));
        }


        public Type GetViewType(string viewTypeName)
            => UiAssemblyExportedTypes[viewTypeName];

        public Type GetViewModelType(Type viewType)
        {
            var viewSuffixIndex = ViewSuffixes.Select(viewSuffix => viewType.Name.LastIndexOf(viewSuffix, StringComparison.Ordinal)).Single(index => index != -1);
            var viewNameWithoutSuffix = viewType.Name.Remove(viewSuffixIndex);
            var viewModelName = String.Concat(viewNameWithoutSuffix, ViewModelNameSuffix);

            return UiAssemblyExportedTypes[viewModelName];
        }


        private static Dictionary<string, Type> GetUiAssemblyExportedTypes(Assembly uiAssembly)
            => uiAssembly.ExportedTypes.ToDictionary(type => type.Name, StringComparer.Ordinal);
    }
}
