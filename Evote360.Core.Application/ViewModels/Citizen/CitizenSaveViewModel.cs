using System.ComponentModel.DataAnnotations;

namespace Evote360.Core.Application.ViewModels.Citizen;

public class CitizenSaveViewModel
{
    public required int Id { get; set; }
    
    [Required(ErrorMessage = "El campo nombre es requerido")]
    [DataType(DataType.Text)]
    public required string FirstName { get; set; }
    
    [Required(ErrorMessage = "El campo apellido es requerido")]
    [DataType(DataType.Text)]
    public required string LastName { get; set; }
    
    [Required(ErrorMessage = "El campo email es requerido")]
    [DataType(DataType.EmailAddress)]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "El campo numero de identidad es requerido")]
    [DataType(DataType.Text)]
    public required string IdentityNumber { get; set; } // Número de documento
    public required bool IsActive { get; set; }
}
