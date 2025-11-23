using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class OperadorResponse
{
    public string Nome { get; set; }
    public string Funcao { get; set; }
    
    public static OperadorResponse ToResponse(Operador operador)
    {
        return new OperadorResponse
        {
            Nome = operador.Nome,
            Funcao = operador.Funcao
        };
    }
}