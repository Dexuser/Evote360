using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class ElectionCandidateEntityConfiguration : IEntityTypeConfiguration<ElectionCandidate>
{
    public void Configure(EntityTypeBuilder<ElectionCandidate> builder)
    {
        builder.ToTable("ElectionCandidates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CandidateId).IsRequired();
        builder.Property(x => x.ElectionId).IsRequired();
        builder.Property(x => x.PoliticalPartyId).IsRequired();
        builder.Property(x => x.ElectivePositionId).IsRequired();
        
       // --- Relationships ---
        builder.HasOne(ec => ec.Election)
            .WithMany(e => e.ElectionCandidates)
            .HasForeignKey(ec => ec.ElectionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ec => ec.Candidate)
            .WithMany()
            .HasForeignKey(ec => ec.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ec => ec.PoliticalParty)
            .WithMany()
            .HasForeignKey(ec => ec.PoliticalPartyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ec => ec.ElectivePosition)
            .WithMany()
            .HasForeignKey(ec => ec.ElectivePositionId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}