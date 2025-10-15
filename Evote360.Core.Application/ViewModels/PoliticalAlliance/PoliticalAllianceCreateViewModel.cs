using System.ComponentModel.DataAnnotations;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.ViewModels.PoliticalParty;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.ViewModels.PoliticalAlliance;

public class PoliticalAllianceCreateViewModel
{
    public required int Id { get; set; }
    public required int RequestingPartyId { get; set; }
    [Required(ErrorMessage = "Debes de seleccionar un partido de destino")]
    public required int TargetPartyId { get; set; }
}
