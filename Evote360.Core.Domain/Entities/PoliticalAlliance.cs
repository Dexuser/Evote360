using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Domain.Entities;

public class PoliticalAlliance
{
    public required int Id { get; set; }

    public required int RequestingPartyId { get; set; }
    public PoliticalParty? RequestingParty { get; set; }

    public required int TargetPartyId { get; set; }
    public PoliticalParty? TargetParty { get; set; }

    public required DateTime RequestDate { get; set; }
    public AllianceStatus Status { get; set; } // Enum: Pending, Accepted, Rejected
    
    public string StatusText
    {
        get
        {
            return Status switch
            {
                AllianceStatus.Accepted => "Aceptada",
                AllianceStatus.Pending => "Pendiente",
                AllianceStatus.Rejected => "Rechazada",
                _ => "Desconocido"
            };
        }
    }
    public DateTime? ResponseDate { get; set; }
}
