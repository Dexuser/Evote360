using System.ComponentModel.DataAnnotations;

namespace Evote360.Core.Application.ViewModels.User;

public class LoginViewModel
{
    [Required(ErrorMessage = "El campo Nombre de usuario es requerido")]
    [DataType(DataType.Text)]
    public required string UserName { get; set; }
    [Required(ErrorMessage = "El campo contraseña es requerido")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }   
}