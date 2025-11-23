using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class EquipeResponse
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    
    public static EquipeResponse ToResponse(Equipe equipe)
    {
        return new EquipeResponse
        {
            Nome = equipe.Nome,
            Descricao = equipe.Descricao
        };
    }
}