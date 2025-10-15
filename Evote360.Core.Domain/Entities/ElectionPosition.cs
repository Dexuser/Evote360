namespace Evote360.Core.Domain.Entities;

// La intencion de esta entidad es ser un historial de los puestos disputados en una eleccion concreta
public class ElectionPosition
{
    public required int Id { get; set; }

    public required int ElectionId { get; set; }
    public Election? Election { get; set; }

    public required int ElectivePositionId { get; set; }
    public ElectivePosition? ElectivePosition { get; set; }
}
