using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAssetRepository Assets { get; }
       
        bool Complete();
    }
}
