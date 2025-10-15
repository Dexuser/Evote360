using System.Text.Json.Serialization.Metadata;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class PoliticalAlianceEntityConfiguration : IEntityTypeConfiguration<PoliticalAlliance>
{
    public void Configure(EntityTypeBuilder<PoliticalAlliance> builder)
    {
        builder.ToTable("PoliticalAlliances");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.RequestingPartyId).IsRequired();
        builder.Property(x => x.TargetPartyId).IsRequired();
        builder.Property(x => x.Status).HasConversion<string>()
            .IsRequired()
            .HasDefaultValue(AllianceStatus.Pending);
        builder.Property(x => x.RequestDate).IsRequired();
        //builder.Property(x => x.ResponseDate);

        builder.HasOne(pa => pa.RequestingParty)
            .WithMany(p => p.RequestAlliances)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(pa => pa.TargetParty)
            .WithMany(p => p.TargetAlliances)
            .OnDelete(DeleteBehavior.Restrict);
        
        // La idea es que no existan pares de registros como (2,1) y (1,2) 
        builder.HasIndex(pa => new { pa.RequestingPartyId, pa.TargetPartyId });


        // La relacion de User a Partido politico esta en partido politico

    }
}