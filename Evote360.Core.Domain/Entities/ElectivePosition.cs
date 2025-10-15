namespace Evote360.Core.Domain.Entities;


public class ElectivePosition
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required bool IsActive { get; set; }

    public ICollection<CandidatePosition> CandidatePositions { get; set; } = [];
}
