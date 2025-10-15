using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Domain.Interfaces;

public interface IPoliticalAllianceRepository : IGenericRepository<PoliticalAlliance>
{
    // Considerar quitar que herede del repositorio Generico y solamente posea dos metodos
    // Agregar alianza y Delete Alianza
    Task AcceptThisAllianceRequest(int politicalAllianceId, bool accept);
}