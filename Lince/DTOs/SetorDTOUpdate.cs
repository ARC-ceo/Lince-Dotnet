using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class SetorDTOUpdate : SetorDTO
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    public static Setor ToEntity(SetorDTOUpdate dto)
    {
        return new Setor
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };
    }
}