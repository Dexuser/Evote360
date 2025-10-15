namespace Evote360.Core.Application.Dtos.User;

public class UserDto
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public int Role { get; set; } // Enum: Administrator, PoliticalLeader
    public required bool IsActive { get; set; }
}