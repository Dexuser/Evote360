using Evote360.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class PoliticalPartyEntityConfiguration: IEntityTypeConfiguration<PoliticalParty>
{
    public void Configure(EntityTypeBuilder<PoliticalParty> builder)
    {
        builder.ToTable("PoliticalParty");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).HasMaxLength(200);
        builder.Property(x => x.Acronym).IsRequired().HasMaxLength(200);
        builder.HasIndex(x => x.Acronym).IsUnique();
        builder.Property(x=> x.LogoPath).HasMaxLength(Int32.MaxValue);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        
        // La relacion de User a Partido politico esta en partido politico
    }
}