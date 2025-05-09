namespace SimpleWallet.Infraestructure.EntityTypesConfigurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleWallet.Domain.Entities;

public class MovementConfiguration : IEntityTypeConfiguration<Movement>
{
    public void Configure(EntityTypeBuilder<Movement> builder)
    {
        builder.ToTable("Movements");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.WalletId)
            .IsRequired()
            .HasColumnName("WalletId")
            .HasColumnType("int");
        builder.HasIndex(m => m.WalletId)
            .HasDatabaseName("IX_Movements_WalletId");
        builder.HasIndex(m => new { m.WalletId, m.CreatedAt });

        builder.Property(m => m.Amount)
            .IsRequired()
            .HasColumnName("Amount")
            .HasPrecision(18, 2);

        builder.Property(m => m.Type)
            .IsRequired()
            .HasColumnName("Type")
            .HasConversion<string>();

        builder.HasOne(m => m.Wallet)
            .WithMany(w => w.Movements)
            .HasForeignKey(m => m.WalletId);
    }
}