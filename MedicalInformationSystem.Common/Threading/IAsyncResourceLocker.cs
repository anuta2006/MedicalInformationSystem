using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MedicalInformationSystem.Common.Threading
{
    public interface IAsyncResourceLocker
    {
        Task<IDisposable> TryGetAccessAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}

