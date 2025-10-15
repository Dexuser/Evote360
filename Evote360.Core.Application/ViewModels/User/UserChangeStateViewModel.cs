using System.ComponentModel.DataAnnotations;

namespace Evote360.Core.Application.ViewModels.User;

public class UserChangeStateViewModel
{
    public required int Id { get; set; }
    public required bool IsActive { get; set; }
}
