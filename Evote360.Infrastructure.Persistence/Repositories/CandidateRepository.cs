using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class CandidateRepository : GenericRepository<Candidate>, ICandidateRepository
{
    public CandidateRepository(VoteContext context) : base(context)
    {
    }
    public async Task SetActiveAsync(int id, bool isActive)
    {
        var entity = await Context.Set<Candidate>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            entity.IsActive = isActive;
            await Context.SaveChangesAsync();
        }
    }

    public async Task<List<Candidate>> GetAllTActiveAsync()
    {
        return await Context.Set<Candidate>().Where(x => x.IsActive).ToListAsync();
    }

    public async Task<bool> IsActiveAsync(int id)
    {
        var candidate = await Context.Set<Candidate>().FindAsync(id);
        if (candidate == null)
        {
            return false;
        }
        return candidate.IsActive;
    }
}