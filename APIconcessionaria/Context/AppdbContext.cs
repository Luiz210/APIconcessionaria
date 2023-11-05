using Microsoft.EntityFrameworkCore;
using APIconcessionaria.Models;

namespace APIconcessionaria.Context
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
            public DbSet<ImagemCarro> ImagensCarros { get; set; }
            public DbSet<Orders> Orders { get; set; }
            public DbSet<Usuario> Usuarios { get; set; }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<Carro>()
                    .Property(c => c.Preco)
                    .HasColumnType("decimal(18,2)");

                base.OnModelCreating(modelBuilder);
            

                modelBuilder.Entity<Usuario>()
                    .Property(c => c.Tipo)
                    .HasMaxLength(1);

                    
                modelBuilder.HasAnnotation("SqlServer:CheckConstraint:CK_Usuario_Tipo", "Tipo IN (0, 1)");
            }
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