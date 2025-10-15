using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class ElectionRepository : GenericRepository<Election>, IElectionRepository
{
    public ElectionRepository(VoteContext context) : base(context)
    {
    }
}