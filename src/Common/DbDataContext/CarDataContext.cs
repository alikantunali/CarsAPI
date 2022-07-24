using Common.Entities;
using Microsoft.EntityFrameworkCore;

namespace Common.DbDataContext
{
    public class CarDataContext : DbContext
    {
        public CarDataContext() { }
        public CarDataContext(DbContextOptions<CarDataContext> options) : base(options)
        {           
        }
        public virtual DbSet<Car> Cars { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<Car>().HasData(
               new Car
               {
                   Id = 1,
                   BrandName = "LAMBORGINI",
                   ManufactureYear = "1998",
                   Model = "COUNTACH"
               },
              new Car
              {
                  Id = 2,
                  BrandName = "PORSCHE",
                  ManufactureYear = "1976",
                  Model = "911 TURBO"
              },
              new Car
              {
                  Id = 3,
                  BrandName = "FORD",
                  ManufactureYear = "1968",
                  Model = "MUSTANG"
              },
              new Car
              {
                  Id = 4,
                  BrandName = "HONDA",
                  ManufactureYear = "2001",
                  Model = "CIVIC"
              },
              new Car
              {
                  Id = 5,
                  BrandName = "JEEP",
                  ManufactureYear = "2019",
                  Model = "RUBICON"
              },
              new Car
              {
                  Id = 6,
                  BrandName = "SUBARU",
                  ManufactureYear = "1999",
                  Model = "IMPREZA"
              },
              new Car
              {
                  Id = 7,
                  BrandName = "CHEVROLET",
                  ManufactureYear = "2004",
                  Model = "CORVETTE"
              },
              new Car
              {
                  Id = 8,
                  BrandName = "FERRARI",
                  ManufactureYear = "1997",
                  Model = "F40"
              },
              new Car
              {
                  Id = 9,
                  BrandName = "DODGE",
                  ManufactureYear = "2013",
                  Model = "CHARGER"
              },
              new Car
              {
                  Id = 10,
                  BrandName = "MAZDA",
                  ManufactureYear = "1998",
                  Model = "RX-3"
              },
              new Car
              {
                  Id = 11,
                  BrandName = "MERCEDES",
                  ManufactureYear = "2010",
                  Model = "G-CLASS"
              },
              new Car
              {
                  Id = 12,
                  BrandName = "DODGE",
                  ManufactureYear = "2002",
                  Model = "VIPER SRT"
              },
              new Car
              {
                  Id = 13,
                  BrandName = "TOYOTA",
                  ManufactureYear = "1999",
                  Model = "Supra"
              },
              new Car
              {
                  Id = 14,
                  BrandName = "HONDA",
                  ManufactureYear = "2002",
                  Model = "S2000"
              },
              new Car
              {
                  Id = 15,
                  BrandName = "BMW",
                  ManufactureYear = "2022",
                  Model = "M5"
              });
            base.OnModelCreating(modelBuilder);
        }

    }

}
