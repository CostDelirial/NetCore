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

    public DbSet<ductos> ductos { get; set; }
    
    }
}
