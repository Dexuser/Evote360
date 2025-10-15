using Evote360.Core.Application.Dtos.PartyLeaderAssignment;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Interfaces;

public interface IPartyLeaderAssignmentServices : IGenericServices<PartyLeaderAssignmentDto>
{
    Task<Result<List<UserDto>>> GetAllAvailableAndActivePartyLeaders();
    Task<Result<PoliticalPartyDto>> GetPoliticalPartyAsocciated(int userId);
}