using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolbetIntegration.API.Features.Transactions;

namespace PoolbetIntegration.API.Data;

public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transaction");

        builder.HasKey(x => x.TransactionId);

        builder.Property(x => x.Status)
            .HasColumnType("int")
            .IsRequired(true);

        builder.Property(x => x.Value)
            .HasColumnType("numeric(10,2)")
            .IsRequired(true);

        builder.Property(x => x.BetUuiId)
            .HasColumnType("varchar(100)")
            .IsRequired(true);
    }
}
