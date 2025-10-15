using System.ComponentModel.DataAnnotations;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.ViewModels.Election;

public class ElectionCreateViewModel
{
    public required int Id { get; set; }
    [Required(ErrorMessage = "El campo nombre es requerido")]
    [DataType(DataType.Text)]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "El campo fecha de realizacion es requerido")]
    [DataType(DataType.Date)]
    public required DateTime Date { get; set; }
}
