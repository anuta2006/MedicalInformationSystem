using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Wpf.ViewViewModelTypeResolver
{
    public interface IViewViewModelTypeResolver
    {
        Type GetViewType(string viewTypeName);

        Type GetViewModelType(Type viewType);
    }
}
