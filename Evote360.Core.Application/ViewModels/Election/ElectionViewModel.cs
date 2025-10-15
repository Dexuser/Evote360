using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.ViewModels.Election;

public class ElectionViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Date { get; set; }
    public ElectionStatus Status { get; set; } // Enum: InProgress, Finished
}
