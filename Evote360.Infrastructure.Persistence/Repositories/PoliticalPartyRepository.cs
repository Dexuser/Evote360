using Evote360.Core.Domain;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class PoliticalPartyRepository : GenericRepository<PoliticalParty>, IPoliticalPartyRepository
{
    public PoliticalPartyRepository(VoteContext context) : base(context)
    {
    }
    
    public async Task SetActiveAsync(int id, bool isActive)
    {
        var entity = await Context.Set<PoliticalParty>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            entity.IsActive = isActive;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<bool> ThisAcronymExistsAsync(string acronym, int excludetThisId)
    {
        return await Context.Set<PoliticalParty>().AnyAsync(x => x.Acronym == acronym && x.Id != excludetThisId);
    }
    
    public async Task<List<PoliticalParty>> GetAllTActiveAsync()
    {
        return await Context.Set<PoliticalParty>().Where(x => x.IsActive).ToListAsync();
    }
    
    public async Task<bool> IsActiveAsync(int id)
    {
        var politicalParty = await Context.Set<PoliticalParty>().FindAsync(id);
        if (politicalParty == null)
        {
            return false;
        }
        return politicalParty.IsActive;
    }
}