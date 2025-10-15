namespace Evote360.Core.Application.ViewModels.PoliticalParty;


public class PoliticalPartyViewModel
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Acronym { get; set; }
    public string? Description { get; set; }
    public string? LogoPath { get; set; }
    public bool IsActive { get; set; }
}
