using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.ViewModels.PoliticalParty;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.ViewModels.PoliticalAlliance;

public class PoliticalAllianceViewModel
{
    public required int Id { get; set; }
    public required int RequestingPartyId { get; set; }
    public PoliticalPartyViewModel? RequestingParty { get; set; }
    public required int TargetPartyId { get; set; }
    public PoliticalPartyViewModel? TargetParty { get; set; }
    public required DateTime RequestDate { get; set; }
    
    public required AllianceStatus Status {get;set;}
    
    public required string StatusText { get; set; } // Enum: Pending, Accepted, Rejected
    public DateTime? ResponseDate { get; set; }
}
