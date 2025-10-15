namespace Evote360.Core.Domain.Entities;

public class Vote
{
    public required int Id { get; set; }
    public required int CitizenId { get; set; }
    public Citizen? Citizen { get; set; }

    public required int ElectionId { get; set; }
    public Election? Election { get; set; }

    public required int ElectivePositionId { get; set; }
    public ElectivePosition? ElectivePosition { get; set; }

    public int? CandidateId { get; set; } // Nullable for "None"
    public Candidate? Candidate { get; set; }

    public DateTime VoteDate { get; set; }
}
