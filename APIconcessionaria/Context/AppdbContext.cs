using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Teste1.Models;

namespace Teste1.Context
{
    public class AppdbContext
    {
        public class AppDbContext : DbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options)
                : base(options)
            {
            }

            public DbSet<Carro> Carros { get; set; }

            public DbSet<Usuario> Usuarios { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", false, true)
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServerConnection"));
            }
        }
    }
}