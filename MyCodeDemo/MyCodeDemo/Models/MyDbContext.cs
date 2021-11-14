using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCodeDemo.Models
{
    public class MyDbContext : DbContext
    {
        #region DbSet <---> Table
        public DbSet<Loai> Loais { get; set; }
        #endregion


        public MyDbContext(DbContextOptions options): base(options)
        {

        }
    }
}
