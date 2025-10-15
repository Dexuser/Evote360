using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class VoteEntityConfiguration : IEntityTypeConfiguration<Vote>
{
    public void Configure(EntityTypeBuilder<Vote> builder)
    {
        builder.ToTable("Votes");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.CitizenId).IsRequired();
        builder.Property(x => x.ElectionId).IsRequired();
        builder.Property(x => x.ElectivePositionId).IsRequired();
        builder.Property(x => x.CandidateId); // Can be null
        builder.Property(x => x.VoteDate).IsRequired();

        // Relation: Citizen 1 -> N Votes
        builder.HasOne(v => v.Citizen)
            .WithMany(c => c.Votes)
            .HasForeignKey(v => v.CitizenId)
            .OnDelete(DeleteBehavior.Restrict); // evita eliminar ciudadano con votos históricos

        // Relation: Election 1 -> N Votes
        builder.HasOne(v => v.Election)
            .WithMany(e => e.Votes)
            .HasForeignKey(v => v.ElectionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation: ElectivePosition 1 -> N Votes
        builder.HasOne(v => v.ElectivePosition)
            .WithMany()
            .HasForeignKey(v => v.ElectivePositionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Relation: Candidate 0..1 -> N Votes
        builder.HasOne(v => v.Candidate)
            .WithMany()
            .HasForeignKey(v => v.CandidateId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(v => v.VoteDate)
            .IsRequired();

        // Un ciudadano solo puede votar una vez por elección y puesto
        builder.HasIndex(v => new { v.CitizenId, v.ElectionId, v.ElectivePositionId })
            .IsUnique();
    }
}


