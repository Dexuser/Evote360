using System.ComponentModel.DataAnnotations;

namespace Evote360.Core.Application.ViewModels.PartyLeaderAssignment;

public class PartyLeaderAssignmentCreateViewModel
{
    public required int Id { get; set; }
    [Required(ErrorMessage = "Debe de seleccionar un dirigente politico")]
    public required int UserId { get; set; }
    
    [Required(ErrorMessage = "Debe de seleccionar un partido politico")]
    public required int PoliticalPartyId { get; set; }
}