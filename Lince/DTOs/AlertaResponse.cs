using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class AlertaResponse
{
    public required string Motivo { get; set; }
    public required string NivelAlerta { get; set; }
    public DateTime DataHora { get; set; }
    
    public static AlertaResponse ToResponse(Alerta alerta)
    {
        return new AlertaResponse
        {
            Motivo = alerta.Motivo,
            NivelAlerta = alerta.NivelAlerta,
            DataHora = alerta.DataHora,
        };
    }
}