using System.ComponentModel.DataAnnotations;

namespace Evote360.Core.Application.ViewModels.User;

public class UserCreateViewModel
{
    public required int Id { get; set; }
    [Required (ErrorMessage = "El campo nombre es requerido")]
    [DataType(DataType.Text)]
    public required string FirstName { get; set; }
    
    [Required (ErrorMessage = "El campo apellido es requerido")]
    [DataType(DataType.Text)]
    public required string LastName { get; set; }
    
    [Required (ErrorMessage = "El campo email es requerido")]
    [DataType(DataType.Text)]
    public required string Email { get; set; }
    
    [Required (ErrorMessage = "El campo nombre de usuario es requerido")]
    [DataType(DataType.Text)]
    public required string UserName { get; set; }
    
    [Required (ErrorMessage = "El campo contraseña es requerido")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    
    [Compare(nameof(Password),ErrorMessage = "Las contraseñas deben de coincidir")]
    [Required (ErrorMessage = "El campo confirmar contraseña es requerido")]
    [DataType(DataType.Password)]
    public required string ConfirmPassword { get; set; }
    
    [Required (ErrorMessage = "el campo rol es requerido")]
    [Range(minimum: 0, maximum: 100)]
    public required int Role { get; set; } // Enum: Administrator, PoliticalLeader
    
    public required bool IsActive { get; set; }
}
