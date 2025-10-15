using Evote360.Core.Domain.Common.Enums;

namespace Evote360.Core.Domain.Entities;

public class Election
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public ElectionStatus Status { get; set; } // Enum: InProgress, Finished

    public ICollection<ElectionParty> ElectionParties { get; set; } = [];
    public ICollection<ElectionPosition> ElectionPositions { get; set; } = [];
    
    public ICollection<ElectionCandidate> ElectionCandidates { get; set; } = [];
    public ICollection<Vote> Votes { get; set; } = [];
}
