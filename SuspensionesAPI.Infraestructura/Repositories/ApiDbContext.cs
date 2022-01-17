using Microsoft.EntityFrameworkCore;
using suspensionesAPI.Core.Models;
using SuspensionesAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SuspensionesAPI.Infraestructura.Repositories
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options): base(options)
        {
            
        }

        public DbSet<cat_ducto> cat_ducto { get; set; }

        public DbSet<usuarios> usuarios { get; set; }

        public DbSet<cat_logistica> cat_logistica { get; set; }

        public DbSet<cat_motivoSuspension> cat_motivoSuspension { get; set; }

        public DbSet<cat_personalCC> cat_personalCC { get; set; }

        public DbSet<suspensiones> suspensiones { get; set; }

       

    }
}
