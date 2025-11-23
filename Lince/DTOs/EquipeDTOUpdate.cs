using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class EquipeDTOUpdate : EquipeDTO
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    public static Equipe ToEntity(EquipeDTOUpdate dto)
    {
        return new Equipe
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };
    }
}