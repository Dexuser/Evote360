using Evote360.Core.Application.Dtos.PoliticalParty;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Interfaces;

public interface IPoliticalPartyServices : IGenericServices<PoliticalPartyDto>, IStateService<PoliticalPartyDto>
{

    Task<Result<List<PoliticalPartyDto>>> GetAllActivePoliticalParties();
}