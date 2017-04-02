using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Common
{
    public class AnonymousDisposable : IDisposable
    {
        private readonly Action _dipose;


        public AnonymousDisposable(Action dipose)
        {
            _dipose = dipose;
        }


        public void Dispose()
        {
            _dipose();
        }
    }
}
