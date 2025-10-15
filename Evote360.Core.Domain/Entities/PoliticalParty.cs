using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Domain;


public class PoliticalParty
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Acronym { get; set; } // TODO UNQ
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    public bool IsActive { get; set; }

    public ICollection<Candidate> Candidates { get; set; } = [];
    public ICollection<PartyLeaderAssignment> Leaders { get; set; } = [];
    
    
    public ICollection<PoliticalAlliance> RequestAlliances { get; set; } = []; // Alianzas donde este partido es el solicitante
    public ICollection<PoliticalAlliance> TargetAlliances { get; set; } = []; // Alianzas donde este partido es el target
}
