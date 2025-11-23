using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class SetorResponse
{
    public string Nome { get; set; }
    public string? Descricao { get; set; }
    
    public static SetorResponse ToResponse(Setor setor)
    {
        return new SetorResponse
        {
            Nome = setor.Nome,
            Descricao = setor.Descricao
        };
    }
}