using Evote360.Core.Domain.Entities;

namespace Evote360.Core.Domain.Interfaces;

public interface IVoteRepository : IGenericRepository<Vote>
{
    // Considerar que solamente se pueda añadir votos. No quitarlos, ni modificarlos ni eliminarlos
}