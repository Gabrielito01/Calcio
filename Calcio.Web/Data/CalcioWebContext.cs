using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Calcio.Web.Data.Entities;

namespace Calcio.Web.Data
{
    public class CalcioWebContext : DbContext
    {
        public CalcioWebContext (DbContextOptions<CalcioWebContext> options)
            : base(options)
        {
        }

        public DbSet<Calcio.Web.Data.Entities.TeamEntity> TeamEntity { get; set; }
    }
}
