using Microsoft.Practices.Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.WpfProject.Navigation
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
