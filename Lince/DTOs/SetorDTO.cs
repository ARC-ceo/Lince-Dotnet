using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class SetorDTO
{
    [Required(ErrorMessage = "O nome para o setor é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public required string Nome { get; set; }
    public string? Descricao { get; set; }

    public static Setor ToEntity(SetorDTO dto)
    {
        return new Setor
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao
        };
    }
}