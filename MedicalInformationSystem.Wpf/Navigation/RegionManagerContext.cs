using Microsoft.Practices.Prism.Regions;
using System;

namespace MedicalInformationSystem.Wpf.Navigation
{
    public static class RegionManagerContext
    {
        private static IRegionManager _current;

        public static IRegionManager Current
        {
            get
            {
                return _current;
            }
            set
            {
                if (_current != null)
                {
                    throw new InvalidOperationException("Region manager context is already set.");
                }

                _current = value;
            }
        }
    }
}
