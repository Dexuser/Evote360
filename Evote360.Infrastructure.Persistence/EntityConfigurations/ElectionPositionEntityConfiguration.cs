using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class ElectionPositionEntityConfiguration : IEntityTypeConfiguration<ElectionPosition>
{
    public void Configure(EntityTypeBuilder<ElectionPosition> builder)
    {
        builder.ToTable("ElectionPosition");
        builder.HasIndex(x => new { x.ElectionId, x.ElectivePositionId }).IsUnique();

        builder.HasOne(x => x.Election)
            .WithMany(e => e.ElectionPositions)
            .HasForeignKey(x => x.ElectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.ElectivePosition)
            .WithMany()
            .HasForeignKey(x => x.ElectivePositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}