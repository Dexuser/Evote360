using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Evote360.Core.Application.ViewModels.PoliticalParty;

public class PoliticalPartyCreateViewModel
{
    public required int Id { get; set; }
    [Required(ErrorMessage = "El campo nombre es requerido")]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "El campo acrónimo es requerido")]
    [DataType(DataType.Text)]   
    public required string Acronym { get; set; }
    
    [DataType(DataType.Text)]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "El campo logo es requerido")]
    [DataType(DataType.Upload)]
    public IFormFile? LogoFile { get; set; }
    
    public bool IsActive { get; set; }
}
