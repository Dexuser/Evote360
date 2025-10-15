using Evote360.Core.Domain;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class ElectivePositionRepository : GenericRepository<ElectivePosition>, IElectivePositionRepository 
{
    public ElectivePositionRepository(VoteContext context) : base(context)
    {
    }
    
        
    public async Task SetActiveAsync(int id, bool isActive)
    {
        var entity = await Context.Set<ElectivePosition>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            entity.IsActive = isActive;
            await Context.SaveChangesAsync();
        }
    }
    
    public async Task<List<ElectivePosition>> GetAllTActiveAsync()
    {
        return await Context.Set<ElectivePosition>().Where(x => x.IsActive).ToListAsync();
    }
    
    public async Task<bool> IsActiveAsync(int id)
    {
        var electivePosition = await Context.Set<ElectivePosition>().FindAsync(id);
        if (electivePosition == null)
        {
            return false;
        }
        return electivePosition.IsActive;
    }
}