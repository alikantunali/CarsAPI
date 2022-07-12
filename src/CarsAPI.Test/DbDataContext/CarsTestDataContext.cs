using Common.DbDataContext;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarsAPI.Test.DbDataContext
{
    public class CarsTestDataContext : DbContext
    {
        public CarsTestDataContext(DbContextOptions<CarsTestDataContext> options) : base(options)
        {

        }
        public virtual DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //_ = modelBuilder.Entity<Car>().




        }
    }
}
