namespace SimpleWallet.Infraestructure.Context;

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SimpleWallet.Domain.Entities;

public class SimpleWalletDbContext(IConfiguration configuration) : DbContext
{
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Movement> Movements { get; set; }

    protected readonly IConfiguration _configuration = configuration;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        if (!string.IsNullOrEmpty(connectionString))
        {
            optionsBuilder.UseNpgsql(connectionString, opt => opt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
        }
        else
        {
            var host = Environment.GetEnvironmentVariable("DATABASE_HOST");
            var database = Environment.GetEnvironmentVariable("DATABASE_NAME");
            var user = Environment.GetEnvironmentVariable("DATABASE_USER");
            var password = Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

            optionsBuilder.UseNpgsql($"Server={host};Database={database};User Id={user};Password={password};", opt => opt.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SimpleWalletDbContext).Assembly);
    }
}