using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class ElectionCandidateRepository : GenericRepository<ElectionCandidate>, IElectionCandidateRepository 
{
    public ElectionCandidateRepository(VoteContext context) : base(context)
    {
    }
}