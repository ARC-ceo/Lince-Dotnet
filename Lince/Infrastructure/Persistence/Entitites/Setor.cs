namespace Lince.Infrastructure.Persistence.Entitites;

public class Setor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    
    // 1..N
    public List<Camera> Cameras { get; set; }
}