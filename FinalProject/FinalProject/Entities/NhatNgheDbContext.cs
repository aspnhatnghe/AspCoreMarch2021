using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Entities
{
    public class NhatNgheDbContext : DbContext
    {
        public NhatNgheDbContext(DbContextOptions<NhatNgheDbContext> options): base(options)
        {

        }
    }
}
