using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class CameraResponse
{
    public string Localizacao { get; set; }
    public string? Descricao { get; set; }
    public string Status { get; set; }
    
    public static CameraResponse ToResponse(Camera camera)
    {
        return new CameraResponse
        {
            Localizacao = camera.Localizacao,
            Descricao = camera.Descricao,
            Status = camera.Status
        };
    }
}