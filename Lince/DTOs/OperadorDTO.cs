using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class OperadorDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public required string Nome { get; set; }
        
    [Required(ErrorMessage = "A funcao é obrigatória")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe uma funcao válida")]
    public required string Funcao { get; set; }
    
    [Required(ErrorMessage = "O ID da Equipe é obrigatório")]
    public Guid EquipeId { get; set; }

    public static Operador ToEntity(OperadorDTO dto)
    {
        return new Operador
        {
            Nome = dto.Nome,
            Funcao = dto.Funcao,
            EquipeId = dto.EquipeId
        };
    }
}