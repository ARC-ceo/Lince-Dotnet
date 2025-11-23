using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class CameraDTO
{
    [Required(ErrorMessage = "A localizacao é obrigatória")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe uma localizacao válida")]
    public required string Localizacao { get; set; }
    
    public string? Descricao { get; set; }
    
    [Required(ErrorMessage = "O status é obrigatório")]
    [RegularExpression("(?i)ATIVO|INATIVO", ErrorMessage = "O status deve ser 'ATIVO' ou 'INATIVO'")]
    public required string Status { get; set; }
    
    [Required(ErrorMessage = "O ID do Setor é obrigatório")]
    public Guid SetorId { get; set; }

    public static Camera ToEntity(CameraDTO dto)
    {
        return new Camera
        {
            Localizacao = dto.Localizacao,
            Descricao = dto.Descricao,
            Status = dto.Status,
            SetorId = dto.SetorId
        };
    }
}