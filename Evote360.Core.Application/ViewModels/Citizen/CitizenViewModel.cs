using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.ViewModels.Citizen;

public class CitizenViewModel
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string IdentityNumber { get; set; } // Número de documento
    public required bool IsActive { get; set; }
}
