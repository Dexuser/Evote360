using System.Numerics;
using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Domain.Interfaces;

public interface IPartyLeaderAssignmentRepository : IGenericRepository<PartyLeaderAssignment>
{
    Task<bool> ThisLeaderExists(int userId);
}