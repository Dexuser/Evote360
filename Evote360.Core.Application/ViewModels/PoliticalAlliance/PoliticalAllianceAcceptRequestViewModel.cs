namespace Evote360.Core.Application.ViewModels.PoliticalAlliance;

public class PoliticalAllianceAcceptRequestViewModel
{
    public required int PoliticalAllianceRequestId { get; set; }
    public required string Name { get; set; }
    public required string Acronym { get; set; }
    public required bool Accepted { get; set; }
}
