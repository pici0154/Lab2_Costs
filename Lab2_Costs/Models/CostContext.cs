using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2_Costs.Models
{
    public class CostContext : DbContext
    {
        public CostContext(DbContextOptions<CostContext> options)
            : base(options)
        {
        }

        public DbSet<CostItem> CostItems { get; set; }


    }
}
