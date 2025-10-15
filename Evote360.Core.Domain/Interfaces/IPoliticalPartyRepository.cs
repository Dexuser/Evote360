namespace Evote360.Core.Domain.Interfaces;

public interface IPoliticalPartyRepository : IGenericRepository<PoliticalParty>, IEntityStateRepository<PoliticalParty>
{
    Task<bool> ThisAcronymExistsAsync(string acronym, int excludetThisId);
}