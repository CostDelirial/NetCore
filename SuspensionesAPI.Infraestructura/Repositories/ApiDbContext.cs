using Microsoft.EntityFrameworkCore;
using suspensionesAPI.Core.Models;
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

    public DbSet<Ducto_cat> Ductos { get; set; }
    public DbSet<Logistica_cat> Logistica { get; set; }
    public DbSet<PersonalCC_cat> PersonalCC { get; set; }
    public DbSet<MotivoSuspension_cat> MotivoSuspension { get; set; }
    public DbSet<Suspensiones> Suspensions { get; set; }
    }
}
