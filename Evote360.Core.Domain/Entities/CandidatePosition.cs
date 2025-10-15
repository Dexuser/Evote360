namespace Evote360.Core.Domain.Entities;


public class CandidatePosition
{
    public required int Id { get; set; }

    public required int CandidateId { get; set; }
    public Candidate? Candidate { get; set; }

    public required int ElectivePositionId { get; set; }
    public ElectivePosition? ElectivePosition { get; set; }

    public required int PoliticalPartyId { get; set; } // Contempla las alianzas.
    public PoliticalParty? PoliticalParty { get; set; }
}
