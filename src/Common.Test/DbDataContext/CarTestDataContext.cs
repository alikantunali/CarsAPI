using Common.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Test.DbDataContext
{
    public class CarTestDataContext : DbContext
    {
        public virtual DbSet<Car> MockCars  { get; set; }
    }

    
}
