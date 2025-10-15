using Evote360.Core.Domain;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class CitizenRepository : GenericRepository<Citizen>, ICitizenRepository
{
    public CitizenRepository(VoteContext context) : base(context)
    {
    }
    public async Task SetActiveAsync(int id, bool isActive)
    {
        var entity = await Context.Set<Citizen>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            entity.IsActive = isActive;
            await Context.SaveChangesAsync();
        }
    }

    public Task<bool> ThisEmailExistsAsync(string email,  int excludeThisId)
    {
        return Context.Set<Citizen>().AnyAsync(x => x.Email == email && x.Id != excludeThisId);
    }
    
    public Task<bool> ThisIdentityNumberExistsAsync(string identityNumber,  int excludeThisId)
    {
        return Context.Set<Citizen>().AnyAsync(x => x.IdentityNumber == identityNumber  && x.Id != excludeThisId);
    }
    
    public async Task<List<Citizen>> GetAllTActiveAsync()
    {
        return await Context.Set<Citizen>().Where(x => x.IsActive).ToListAsync();
    }
    
    public async Task<bool> IsActiveAsync(int id)
    {
        var citizen = await Context.Set<Citizen>().FindAsync(id);
        if (citizen == null)
        {
            return false;
        }
        return citizen.IsActive;
    }
    
    
}