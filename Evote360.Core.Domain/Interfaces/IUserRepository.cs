namespace Evote360.Core.Domain.Interfaces;

public interface IUserRepository : IGenericRepository<User>, IEntityStateRepository<User>
{
    Task<User?> Login(string username, string password);
    Task<bool> ThisUserNameExitsAsync(string username, int excludeThisId);
}