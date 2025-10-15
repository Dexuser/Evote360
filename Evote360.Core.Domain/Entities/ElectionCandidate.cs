namespace Evote360.Core.Domain.Entities;

public class ElectionCandidate
{
    public required int Id { get; set; }
    public required int ElectionId { get; set; }
    public Election? Election { get; set; }
    public required int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }
    public required int PoliticalPartyId { get; set; }
    public PoliticalParty? PoliticalParty { get; set; }
    
    public required int ElectivePositionId { get; set; }
    public ElectivePosition? ElectivePosition { get; set; }
    
}