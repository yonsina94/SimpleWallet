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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new WalletConfiguration());
        modelBuilder.ApplyConfiguration(new MovementConfiguration());
    }
}