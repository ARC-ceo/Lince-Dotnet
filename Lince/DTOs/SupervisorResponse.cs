using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class SupervisorResponse
{
    public string Nome { get; set; }
    public string Email { get; set; }
    
    public static SupervisorResponse ToResponse(Supervisor supervisor)
    {
        return new SupervisorResponse
        {
            Nome = supervisor.Nome,
            Email = supervisor.Email
        };
    }
}