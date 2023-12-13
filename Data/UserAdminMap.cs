using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PoolbetIntegration.API.Features.UserAdmins;

namespace PoolbetIntegration.API.Data;

public class UserAdminMap : IEntityTypeConfiguration<UserAdmin>
{
    public void Configure(EntityTypeBuilder<UserAdmin> builder)
    {
        builder.ToTable("UserAdmin");

        builder.HasKey(x => x.UserAdminId);

        builder.Property(x => x.Username)
               .HasColumnName("Username")
               .HasColumnType("varchar")
               .HasMaxLength(100)
               .IsRequired(true);

        builder.Property(x => x.Name)
               .HasColumnName("Name")
               .HasColumnType("varchar")
               .HasMaxLength(100)
               .IsRequired(true);

        builder.Property(x => x.Email)
               .HasColumnName("Email")
               .HasColumnType("varchar")
               .HasMaxLength(100)
               .IsRequired(true);

        builder.Property(x => x.Credit)
               .HasColumnName("Credit")
               .HasColumnType("decimal(10,2)")
               .IsRequired(true);
    }
}
