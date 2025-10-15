using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Domain;

public class User
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string UserName { get; set; }
    public required string PasswordHash { get; set; }
    public UserRole Role { get; set; } // Enum: Administrator, PoliticalLeader
    public required bool IsActive { get; set; }
}
