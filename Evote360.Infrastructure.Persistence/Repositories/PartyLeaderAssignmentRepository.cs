using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class PartyLeaderAssignmentRepository : GenericRepository<PartyLeaderAssignment>, IPartyLeaderAssignmentRepository
{
    public PartyLeaderAssignmentRepository(VoteContext context) : base(context)
    {
    }

    public async Task<bool> ThisLeaderExists(int userId)
    {
        return await Context.Set<PartyLeaderAssignment>().AnyAsync(pla => pla.UserId == userId);
    }
}