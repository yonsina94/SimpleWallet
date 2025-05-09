namespace SimpleWallet.Infraestructure.Context;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SimpleWallet.Domain.Entities;
using SimpleWallet.Infraestructure.EntityTypesConfigurations;
using Microsoft.Extensions.Configuration;

public class SimpleWalletDbContext(DbContextOptions<SimpleWalletDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Movement> Movements { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connectionString = _configuration.GetConnectionString("DefaultConnection");
    //     if (!string.IsNullOrEmpty(connectionString))
    //     {
    //         optionsBuilder.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
    //     }
    //     else
    //     {
    //         var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
    //         var database = Environment.GetEnvironmentVariable("DATABASE_NAME");
    //         var user = Environment.GetEnvironmentVariable("DATABASE_USER");
    //         var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

    //         optionsBuilder.UseNpgsql($"Server={host};Database={database};User Id={user};Password={password};", opt => opt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new WalletConfiguration());
        modelBuilder.ApplyConfiguration(new MovementConfiguration());
    }
}

// public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SimpleWalletDbContext>
// {
//     public SimpleWalletDbContext CreateDbContext(string[] args)
//     {
//         // Build configuration
//         IConfigurationRoot configuration = new ConfigurationBuilder()
//             .SetBasePath(Directory.GetCurrentDirectory())
//             .AddJsonFile("appsettings.json") // Asegúrate de que el nombre del archivo es correcto
//             .Build();

//         // Get connection string
//         var connectionString = configuration.GetConnectionString("DefaultConnection"); // Ajusta el nombre

//         // Build DbContextOptions
//         var builder = new DbContextOptionsBuilder<SimpleWalletDbContext>();
//         builder.UseNpgsql(connectionString); // O el proveedor de base de datos que estés usando

//         // Create DbContext
//         return new SimpleWalletDbContext(builder.Options, configuration); // Asegúrate de pasar la configuration
//     }
// }