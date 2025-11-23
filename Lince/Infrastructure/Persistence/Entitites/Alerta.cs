namespace Lince.Infrastructure.Persistence.Entitites;

public class Alerta
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Motivo { get; set; }
    public string NivelAlerta { get; set; }
    public DateTime DataHora { get; set; }
    
    // N..1
    public Guid OperadorId { get; set; }
    public Operador Operador { get; set; }
    
    // N..1
    public Guid CameraId { get; set; }
    public Camera Camera { get; set; }
}