using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.ViewModels.Citizen;

public class CitizenChangeStateViewModel
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required bool IsActive { get; set; }
}
