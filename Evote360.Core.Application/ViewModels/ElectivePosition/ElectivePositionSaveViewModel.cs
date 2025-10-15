using System.ComponentModel.DataAnnotations;

namespace Evote360.Core.Application.ViewModels.ElectivePosition;

public class ElectivePositionSaveViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo nombre es requerido")]
    [DataType(DataType.Text)]
    public required string Name { get; set; }

    [Required(ErrorMessage = "El campo descripción es requerido")]
    [DataType(DataType.MultilineText)]
    public required string Description { get; set; }
    
    public required bool IsActive { get; set; }   
}