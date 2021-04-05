using Hahn.ApplicationProcess.February2021.Domain.interfaces;
using Hahn.ApplicationProcess.February2021.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.EfRepository
{
    public class AssetRepository:Repository<Asset>,IAssetRepository
    {
        public AssetRepository(HahnDbContext context):base(context)
        {

        }
    }
}
