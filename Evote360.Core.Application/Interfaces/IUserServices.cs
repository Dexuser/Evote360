using Evote360.Core.Application.Dtos.User;
using Evote360.Core.Domain.Interfaces;

namespace Evote360.Core.Application.Interfaces;

public interface IUserServices : IGenericServices<UserDto>, IStateService<UserDto>
{
    Task<Result<UserDto>> LoginAsync(LoginDto loginDto);
    
}