using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class ElectivePositionEntityConfiguration : IEntityTypeConfiguration<ElectivePosition>
{
    public void Configure(EntityTypeBuilder<ElectivePosition> builder)
    {
        builder.ToTable("ElectivePositions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
    }
}