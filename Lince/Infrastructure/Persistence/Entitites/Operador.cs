namespace Lince.Infrastructure.Persistence.Entitites;

public class Operador
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Funcao { get; set; }
    
    // N..1
    public Guid EquipeId { get; set; }
    public Equipe Equipe { get; set; }
    
    // 1..N
    public List<Alerta> Alertas { get; set; }
}