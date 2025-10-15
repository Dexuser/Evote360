namespace Evote360.Core.Application.ViewModels.ElectivePosition;

public class ElectivePositionViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }   
}