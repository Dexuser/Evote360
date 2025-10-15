using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Application.ViewModels.Election;

public class ElectionSummaryViewModel
{
    // Se proyecta desde un servicio
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required DateTime Date { get; set; }
    public ElectionStatus Status { get; set; } // Enum: InProgress, Finished
    
    public string StatusText
    {
        get
        {
            return Status switch
            {
                ElectionStatus.InProgress => "En progreso",
                ElectionStatus.Finished => "Finalizada",
                _ => "Desconocido"
            };
        }
    }
    public required int PoliticalPartiesCount { get; set; }
    public required int ElectivePositionsCount { get; set; }
}
