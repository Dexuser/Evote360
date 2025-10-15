using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class CandidateEntityConfiguration : IEntityTypeConfiguration<Candidate>
{
    public void Configure(EntityTypeBuilder<Candidate> builder)
    {
        builder.ToTable("Candidates");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.PhotoPath).HasMaxLength(100);
        builder.Property(x => x.PoliticalPartyId).IsRequired();
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        builder.HasOne(c => c.PoliticalParty)
            .WithMany(p => p.Candidates)
            .HasForeignKey(c => c.PoliticalPartyId)
            .OnDelete(DeleteBehavior.Restrict);;

    }
}