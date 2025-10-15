using Evote360.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Evote360.Infrastructure.Persistence.EntityConfigurations;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FirstName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.LastName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
        builder.Property(x => x.UserName).IsRequired().HasMaxLength(100);
        builder.Property(x => x.PasswordHash).IsRequired().HasMaxLength(100);
        builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(x => x.Role).HasConversion<string>().IsRequired();
        
        // La relacion de User a Partido politico esta en partido politico

    }
}