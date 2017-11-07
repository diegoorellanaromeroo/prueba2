using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Elmonte.Models
{
    public class CarcelDbContext : DbContext
    {
        public DbSet<Preso> Presos { get; set; }
        public DbSet<Juez> Juezes { get; set; }
        public DbSet<CondenaDelito> CondenaDelitos { get; set; }
        public DbSet<Condena> Condenas { get; set; }
        public DbSet<Delito> Delitos { get; set; }
    }
}