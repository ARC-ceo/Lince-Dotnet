using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class CameraDTOUpdate : CameraDTO
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    public static Camera ToEntity(CameraDTOUpdate dto)
    {
        return new Camera
        {
            Id = dto.Id,
            Localizacao = dto.Localizacao,
            Descricao = dto.Descricao,
            Status = dto.Status,
            SetorId = dto.SetorId
        };
    }
}