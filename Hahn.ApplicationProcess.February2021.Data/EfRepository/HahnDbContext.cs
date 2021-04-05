using Hahn.ApplicationProcess.February2021.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Data.EfRepository
{
   public class HahnDbContext:DbContext
    {
        public HahnDbContext(DbContextOptions<HahnDbContext> options):base(options)
        {

        }
        public DbSet<Asset> Assets { get; set; }
    }
}
