namespace Evote360.Core.Domain.Entities;


public class Candidate
{
    public required int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PhotoPath { get; set; }
    public required bool IsActive { get; set; }

    public required int PoliticalPartyId { get; set; }
    public PoliticalParty? PoliticalParty { get; set; }

    public ICollection<CandidatePosition> CandidatePositions { get; set; } = [];
    
}
