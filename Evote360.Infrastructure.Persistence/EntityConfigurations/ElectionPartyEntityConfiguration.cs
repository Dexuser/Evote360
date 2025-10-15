using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class ElectionPartyEntityConfiguration : IEntityTypeConfiguration<ElectionParty>
{

    public void Configure(EntityTypeBuilder<ElectionParty> builder)
    {
        builder.ToTable("ElectionParty");
        builder.HasIndex(x => new { x.ElectionId, x.PoliticalPartyId }).IsUnique();

        builder.HasOne(x => x.Election)
            .WithMany(e => e.ElectionParties)
            .HasForeignKey(x => x.ElectionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.PoliticalParty)
            .WithMany()
            .HasForeignKey(x => x.PoliticalPartyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}