using System.ComponentModel.DataAnnotations;
using Evote360.Core.Application.Dtos.Candidate;
using Evote360.Core.Application.Dtos.ElectivePosition;
using Evote360.Core.Application.Dtos.PoliticalParty;

namespace Evote360.Core.Application.ViewModels.CandidatePosition;


public class CandidatePositionCreateViewModel
{
    public required int Id { get; set; } // hidden
    [Required(ErrorMessage = "Debe de seleccionar un candidato")]
    public required int CandidateId { get; set; }
    [Required(ErrorMessage = "Debe de seleccionar un puesto electivo")]
    public required int ElectivePositionId { get; set; } // hidden
    public required int PoliticalPartyId { get; set; } // Contempla las alianzas. Hidden
}
