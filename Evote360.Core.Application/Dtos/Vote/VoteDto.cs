namespace Evote360.Core.Application.Dtos.Vote;

public class VoteDto
{
    public required int Id { get; set; }
    public required int CitizenId { get; set; }
    public required int ElectionId { get; set; }
    public required int ElectivePositionId { get; set; }
    public int? CandidateId { get; set; } // Nullable for "None"
    public DateTime VoteDate { get; set; }
}
