using Evote360.Core.Domain;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class ElectionEntityConfiguration : IEntityTypeConfiguration<Election>
{
    public void Configure(EntityTypeBuilder<Election> builder)
    {
        builder.ToTable("Elections");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Date).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Status)
            .HasConversion<string>()
            .IsRequired()
            .HasMaxLength(100);
        
        // La relacion de User a Partido politico esta en partido politico

    }
}