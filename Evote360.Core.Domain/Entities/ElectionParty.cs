namespace Evote360.Core.Domain.Entities;


// La intencion de esta entidad es ser un historial de los partidos que disputaron en una eleccion concreta
public class ElectionParty
{
    public int Id { get; set; }

    public int ElectionId { get; set; }
    public Election? Election { get; set; }

    public int PoliticalPartyId { get; set; }
    public PoliticalParty? PoliticalParty { get; set; }
}
