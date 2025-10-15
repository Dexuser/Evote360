using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class CandidatePositionEntityConfiguration : IEntityTypeConfiguration<CandidatePosition>
{
    public void Configure(EntityTypeBuilder<CandidatePosition> builder)
    {
        builder.ToTable("CandidatePositions");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CandidateId).IsRequired();
        builder.Property(x => x.ElectivePositionId).IsRequired();
        builder.Property(x => x.ElectivePositionId).IsRequired();
        builder.Property(x => x.PoliticalPartyId).HasMaxLength(200);

        builder.HasOne(cp => cp.Candidate)
            .WithMany(c => c.CandidatePositions)
            .HasForeignKey(x => x.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cp => cp.ElectivePosition)
            .WithMany(e => e.CandidatePositions)
            .HasForeignKey(x => x.ElectivePositionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(cp => cp.PoliticalParty)
            .WithMany()
            .HasForeignKey(x => x.PoliticalPartyId)
            .OnDelete(DeleteBehavior.Restrict);


        // La relacion de User a Partido politico esta en partido politico

    }
}