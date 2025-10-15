using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;

namespace Evote360.Core.Application.Dtos.PartyLeaderAssignment;


public class PartyLeaderAssignmentDto
{
    public required int Id { get; set; }
    public required int UserId { get; set; } 
    public UserDto? User { get; set; }
    public required int PoliticalPartyId { get; set; }
    public PoliticalPartyDto? PoliticalParty { get; set; }
}
