using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class VoteRepository : GenericRepository<Vote>, IVoteRepository
{
    public VoteRepository(VoteContext context) : base(context)
    {
        // Considerar solamente dejar que se Añadan votos, osea un solo metodo ADD
    }
}