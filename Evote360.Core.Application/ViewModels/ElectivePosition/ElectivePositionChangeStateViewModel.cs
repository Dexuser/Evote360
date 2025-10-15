namespace Evote360.Core.Application.ViewModels.ElectivePosition;

public class ElectivePositionChangeStateViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required bool IsActive { get; set; }   
}