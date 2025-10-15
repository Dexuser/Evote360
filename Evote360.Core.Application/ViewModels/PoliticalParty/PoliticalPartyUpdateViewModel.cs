using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Evote360.Core.Application.ViewModels.PoliticalParty;


public class PoliticalPartyUpdateViewModel
{
    public required int Id { get; set; }
    
    [Required]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [Required]
    [DataType(DataType.Text)]   
    public required string Acronym { get; set; }
    
    [DataType(DataType.Text)]
    public string? Description { get; set; }
    
    [DataType(DataType.Upload)]
    public IFormFile? LogoFile { get; set; }
    
    public bool IsActive { get; set; }
}
