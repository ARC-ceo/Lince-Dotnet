namespace Lince.Infrastructure.Persistence.Entitites;

public class Equipe
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    
    // 1..N
    public List<Supervisor> Supervisores { get; set; }
    
    // 1..N
    public List<Operador> Operadores { get; set; }
}