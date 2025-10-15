using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class CitizenEntityConfiguration : IEntityTypeConfiguration<Citizen>
{
    public void Configure(EntityTypeBuilder<Citizen> builder)
    {
        builder.ToTable("Citizens");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IdentityNumber).IsRequired().HasMaxLength(12);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        
        builder.HasIndex(x => new { x.IdentityNumber }).IsUnique();
        builder.HasIndex(x => new { x.Email }).IsUnique();
        
        // La relacion de User a Partido politico esta en partido politico

    }
}