namespace Evote360.Core.Domain.Entities;


public class PartyLeaderAssignment
{
    public required int Id { get; set; }

    public required int UserId { get; set; } // TODO UNQ
    public User? User { get; set; }

    public required int PoliticalPartyId { get; set; }
    public PoliticalParty? PoliticalParty { get; set; }

    // Restrict one assignment per user
}
