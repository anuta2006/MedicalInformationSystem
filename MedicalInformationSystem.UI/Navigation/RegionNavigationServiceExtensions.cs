using MedicalInformationSystem.Common.Extensions;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.UI.Navigation
{
    public static class RegionNavigationServiceExtensions
    {
        public static void Navigate(this IRegionNavigationService regionNavigationService, string regionName, string viewToken)
            => regionNavigationService.Region.RegionManager.Regions[regionName].RequestNavigate(new Uri(viewToken, UriKind.Relative));

        public static void Navigate(this IRegionNavigationService regionNavigationService, string regionName, string viewToken, IReadOnlyDictionary<string, object> parameters)
        {
            var navigationParameters = new NavigationParameters();
            parameters.ForEach(p => navigationParameters.Add(p.Key, p.Value));

            regionNavigationService.Region.RegionManager.Regions[regionName].RequestNavigate(new Uri(viewToken, UriKind.Relative),
                navigationParameters);
        }
        public static void ClearNavigationHistory(this IRegionNavigationService regionNavigationService, string regionName)
            => regionNavigationService.Region.RegionManager.Regions[regionName].NavigationService.Journal.Clear();
    }
}
