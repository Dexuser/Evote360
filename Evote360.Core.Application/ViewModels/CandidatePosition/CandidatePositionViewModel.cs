using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Dtos.PoliticalParty;

namespace Evote360.Core.Application.ViewModels.CandidatePosition;


public class CandidatePositionViewModel
{
    public required int Id { get; set; }
    public required int CandidateId { get; set; }
    public CandidateDto? Candidate { get; set; }
    public required int ElectivePositionId { get; set; }
    public ElectivePositionDto? ElectivePosition { get; set; }
    public required int PoliticalPartyId { get; set; } // Contempla las alianzas.
    public PoliticalPartyDto? PoliticalParty { get; set; }
    public bool IsActive { get; set; }
}
