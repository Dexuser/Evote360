using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class CandidatePositionRepository : GenericRepository<CandidatePosition>, ICandidatePositionRepository 
{
    public CandidatePositionRepository (VoteContext context) : base(context)
    {
    }

    
}