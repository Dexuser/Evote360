using System.ComponentModel.DataAnnotations;
using Evote360.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Evote360.Core.Application.ViewModels.Candidate;


public class CandidateUpdateViewModel
{
    public required int Id { get; set; }
    [Required(ErrorMessage = "El campo nombre es requerido")]
    [DataType(DataType.Text)]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "El campo apellido es requerido")]
    [DataType(DataType.Text)]
    public required string LastName { get; set; }
    
    [DataType(DataType.Upload)]
    public IFormFile? PhotoFile { get; set; }
    
    
    public required int PoliticalPartyId { get; set; }
    public required bool IsActive { get; set; }

}
