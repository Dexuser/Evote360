using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.ViewModels.Candidate;

public class CandidateChangeStateViewModel
{
    public required int Id { get; set; }
    public required bool IsActive { get; set; }
}
