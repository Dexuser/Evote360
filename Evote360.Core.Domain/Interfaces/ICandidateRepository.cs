using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Domain.Interfaces;

public interface ICandidateRepository : IGenericRepository<Candidate>,
    IEntityStateRepository<Candidate>
{
}