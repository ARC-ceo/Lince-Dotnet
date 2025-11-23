using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class AlertaDTO
{
    [Required(ErrorMessage = "O motivo é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um motivo válido")]
    public required string Motivo { get; set; }
    
    [Required(ErrorMessage = "O motivo é obrigatório")]
    [StringLength(20, MinimumLength = 2, ErrorMessage = "Informe um motivo válido")]
    public required string NivelAlerta { get; set; }
    
    [Required(ErrorMessage = "O ID do Operador é obrigatório")]
    public Guid OperadorId { get; set; }
    
    [Required(ErrorMessage = "O ID da camera é obrigatório")]
    public Guid CameraId { get; set; }

    public static Alerta ToEntity(AlertaDTO dto)
    {
        return new Alerta
        {
            Motivo = dto.Motivo,
            NivelAlerta = dto.NivelAlerta,
            DataHora = DateTime.Now,
            OperadorId = dto.OperadorId,
            CameraId = dto.CameraId
        };
    }
}