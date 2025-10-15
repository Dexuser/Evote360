using Evote360.Core.Application.Dtos.Citizen;
using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Interfaces;

public interface ICitizenServices : IGenericServices<CitizenDto>, IStateService<CitizenDto>
{
}