namespace Lince.Infrastructure.Persistence.Entitites;

public class Supervisor
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Nome { get; set; }
    public string Email { get; set; }
    public string? Telefone { get; set; }
    
    // N..1
    public Guid? EquipeId { get; set; }
    public Equipe? Equipe { get; set; }
}