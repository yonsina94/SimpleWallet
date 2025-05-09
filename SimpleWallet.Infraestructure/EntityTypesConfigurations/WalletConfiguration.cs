namespace SimpleWallet.Infraestructure.EntityTypesConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWallet.Domain.Entities;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.DocumentId)
            .IsRequired()
            .HasColumnName("DocumentId")
            .HasMaxLength(100);

        builder.Property(w => w.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasMaxLength(100);

        builder.Property(w => w.Balance)
            .IsRequired()
            .HasColumnName("Balance")
            .HasPrecision(18, 2);

        builder.HasMany(w => w.Movements)
            .WithOne(m => m.Wallet)
            .HasForeignKey(m => m.WalletId);
    }
}