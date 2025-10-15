using Evote360.Core.Domain.Common.Enums;
using Evote360.Core.Domain.Entities;
using Evote360.Core.Domain.Interfaces;
using Evote360.Infrastructure.Persistence.Context;

namespace Evote360.Infrastructure.Persistence.Repositories;

public class PoliticalAllianceRepository : GenericRepository<PoliticalAlliance>, IPoliticalAllianceRepository
{
    public PoliticalAllianceRepository(VoteContext context) : base(context)
    {
    }

    public async Task AcceptThisAllianceRequest(int politicalAllianceId, bool accept)
    {
        var request = await Context.Set<PoliticalAlliance>().FindAsync(politicalAllianceId);
        if (request != null)
        {
            request.Status = accept ? AllianceStatus.Accepted : AllianceStatus.Rejected;
            request.ResponseDate = DateTime.Now;
            await Context.SaveChangesAsync();
        }
    }
}