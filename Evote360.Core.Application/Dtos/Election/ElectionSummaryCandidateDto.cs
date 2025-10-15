using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.Dtos.Election;

public class ElectionSummaryCandidateDto
{
    // Estas clases se proyectan desde un servicio.
    public required int Id { get; set; }
    public required string Name { get; set; }
    
    public required int PoliticalPartyCount { get; set; }
    public required int CandidatesCount { get; set; }
    public required int VotesCount { get; set; }
}
