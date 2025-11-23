using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class AlertaDTOUpdate : AlertaDTO
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    public static Alerta ToEntity(AlertaDTOUpdate dto)
    {
        return new Alerta
        {
            Id = dto.Id,
            Motivo = dto.Motivo,
            NivelAlerta = dto.NivelAlerta,
            DataHora = DateTime.Now,
            OperadorId = dto.OperadorId,
            CameraId = dto.CameraId
        };
    }
}