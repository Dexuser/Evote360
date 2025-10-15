namespace Evote360.Core.Application.Interfaces;

public interface IStateService<TDtoModel>
{
    Task SetActiveAsync(int id,bool isActive);
    Task<List<TDtoModel>> GetAllTActiveAsync();
    Task<bool> IsActiveAsync(int id);
}