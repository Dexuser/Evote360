namespace Evote360.Core.Domain.Interfaces;

public interface ICitizenRepository : IGenericRepository<Citizen>, IEntityStateRepository<Citizen>
{
    Task<bool> ThisEmailExistsAsync(string email, int excludeThisId );
    Task<bool> ThisIdentityNumberExistsAsync(string identityNumber, int excludeThisId);
    
}