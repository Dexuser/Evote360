namespace Evote360.Core.Application.Dtos.Citizen;


public class CitizenDto
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string IdentityNumber { get; set; } // Número de documento
    public required bool IsActive { get; set; }
}
