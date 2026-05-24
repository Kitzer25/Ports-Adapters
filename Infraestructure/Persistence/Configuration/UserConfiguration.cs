using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infraestructure.Persistence.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(u => u.Id);
        
        builder.Property(u => u.Id)
            .HasColumnName("user_id");

        builder.Property(u => u.Username)
            .HasConversion(v => v.Value, v => new Username(v))
            .HasColumnName("username")
            .IsRequired();

        builder.Property(u => u.Email)
            .HasConversion(v => v.Value, v => new Email(v))
            .HasColumnName("email");

        builder.Property(u => u.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired();

        builder.Property(u => u.CreatedAt)
            .HasColumnName("created_at");
        
        builder.HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity(j => j.ToTable("user_roles"))
            .Metadata.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}