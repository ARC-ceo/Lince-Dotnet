using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class EquipeDTO
{
    [Required(ErrorMessage = "O nome para a equipe é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public required string Nome { get; set; }
    public string? Descricao { get; set; }

    public static Equipe ToEntity(EquipeDTO dto)
    {
        return new Equipe
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };
    }
}