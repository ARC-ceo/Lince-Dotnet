namespace Lince.Infrastructure.Persistence.Entitites;

public class Camera
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Localizacao { get; set; }
    public string? Descricao { get; set; }
    public string Status { get; set; }
    
    // 1..N
    public List<Alerta> Alertas { get; set; }
    
    // N..1
    public Guid SetorId { get; set; }
    public Setor Setor { get; set; }
}