using System.Diagnostics;
using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.Dtos.Election;

public class ElectionSummaryDto
{
    // Se proyecta desde un servicio
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Date { get; set; }
    public required ElectionStatus Status { get; set; } // Enum: InProgress, Finished

    public int PoliticalPartiesCount { get; set; }
    public int ElectivePositionsCount { get; set; }
}
