using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");
        
        builder.HasKey(r => r.Id);
        
        builder.Property(r => r.Id)
            .HasColumnName("role_id")
            .IsRequired();

        builder.Property(r => r.Name)
            .HasColumnName("role_name")
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasKey(r => r.Id);
    }
}