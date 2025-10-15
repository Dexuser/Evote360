using Evote360.Core.Application.Dtos.PoliticalAlliance;
using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Application.Interfaces;

public interface IPoliticalAllianceServices : IGenericServices<PoliticalAllianceDto>
{
    Task<Result<List<PoliticalAllianceDto>>> GetAllTheRequestsOfThisPoliticalParty(int politicalPartyId);
    Task<Result<List<PoliticalPartyDto>>> GetAllTheAvailablePoliticalParties(int politicalPartyId);
    Task<Result> AcceptThisAllianceRequest(int politicalAllianceId, bool accept);
}