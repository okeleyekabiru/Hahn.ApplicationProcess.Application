using Hahn.ApplicationProcess.February2021.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.EfRepository
{
    public class UnitOfWorkRepository : IUnitOfWork
    {

        private readonly HahnDbContext _context;
        public IAssetRepository Assets { get; }

        public UnitOfWorkRepository(HahnDbContext context, IAssetRepository assetRepository)
        {
            _context = context;
            Assets = assetRepository;
        }

        
        public bool Complete()
        {
            return _context.SaveChanges() > 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
