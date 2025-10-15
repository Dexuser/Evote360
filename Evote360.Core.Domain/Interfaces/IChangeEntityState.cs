namespace Evote360.Core.Domain.Interfaces;

public interface IEntityStateRepository<TEntity>
{
    Task SetActiveAsync(int id, bool isActive);
    Task<List<TEntity>> GetAllTActiveAsync();
    Task<bool> IsActiveAsync(int id);
}