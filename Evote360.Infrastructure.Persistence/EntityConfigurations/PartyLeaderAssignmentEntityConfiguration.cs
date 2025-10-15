using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class PartyLeaderAssignmentEntityConfiguration : IEntityTypeConfiguration<PartyLeaderAssignment>
{
    public void Configure(EntityTypeBuilder<PartyLeaderAssignment> builder)
    {
        builder.ToTable("PartyLeaderAssignments");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.PoliticalPartyId).IsRequired();
        builder.Property(x => x.UserId).IsRequired();
        builder.HasIndex(u => u.UserId).IsUnique(); // Lider en un solo partido.

        // Esta entidad es la relacion entre un usuario dirigente politico y un partido

        builder.HasOne(partyLeader => partyLeader.User)
            .WithOne()
            .HasForeignKey<PartyLeaderAssignment>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(partyLeader => partyLeader.PoliticalParty)
            .WithMany(p => p.Leaders)
            .HasForeignKey(x => x.PoliticalPartyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}