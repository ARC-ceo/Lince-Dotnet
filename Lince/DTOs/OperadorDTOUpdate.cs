using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class OperadorDTOUpdate : OperadorDTO
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    public static Operador ToEntity(OperadorDTOUpdate dto)
    {
        return new Operador
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Funcao = dto.Funcao,
            EquipeId = dto.EquipeId
        };
    }
}