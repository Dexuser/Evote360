namespace Evote360.Core.Domain.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity?> AddAsync(TEntity entity);
    Task<List<TEntity>> AddRangeAsync(List<TEntity> entities);
    Task<TEntity?> UpdateAsync(int id, TEntity entity);
    Task DeleteAsync(int id);
    IQueryable<TEntity> GetAllQueryable(); // desde aqui se haran los includes y las consultas muy especificas
}