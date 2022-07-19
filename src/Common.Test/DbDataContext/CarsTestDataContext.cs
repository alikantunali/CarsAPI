using Common.DbDataContext;
using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.DbDataContext
{
    public class CarsTestDataContext : DbContext
    {
        public CarsTestDataContext()
        {
            
        }
        public virtual DbSet<Car> Cars { get; set; }
       

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var carList= new List<Car>()
            {
                new Car()
                {
                    Id = 1,
                    BrandName="x",
                    ManufactureYear="2022",
                    Model="xx"
                },
                 new Car()
                {
                    Id = 2,
                    BrandName="y",
                    ManufactureYear="2022",
                    Model="yy"
                },
                  new Car()
                {
                    Id = 3,
                    BrandName="z",
                    ManufactureYear="2022",
                    Model="zz"
                },

            };

            _ = modelBuilder.Entity<Car>()
                .Property(b => b.Id == carList.First().Id);       

        }
    }
}
