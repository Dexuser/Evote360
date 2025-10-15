using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.ViewModels.ElectivePosition;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.ViewModels.Candidate;

public class CandidateViewModel
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhotoPath { get; set; }
    public required bool IsActive { get; set; }
    public required int PoliticalPartyId { get; set; }
    
    public ElectivePositionViewModel? ElectivePosition { get; set; }
}
