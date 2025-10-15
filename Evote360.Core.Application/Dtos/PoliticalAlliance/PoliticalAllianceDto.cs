using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.Dtos.PoliticalAlliance;

public class PoliticalAllianceDto
{
    public required int Id { get; set; }
    public required int RequestingPartyId { get; set; }
    public PoliticalPartyDto? RequestingParty { get; set; }
    public required int TargetPartyId { get; set; }
    public PoliticalPartyDto? TargetParty { get; set; }
    public required DateTime RequestDate { get; set; }
    public AllianceStatus Status { get; set; } // Enum: Pending, Accepted, Rejected
    public DateTime? ResponseDate { get; set; }
}
