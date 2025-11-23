using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class SupervisorDTOUpdate : SupervisorDTO
{
    [Required(ErrorMessage = "O ID é obrigatório")]
    public Guid Id { get; set; }
    
    public static Supervisor ToEntity(SupervisorDTOUpdate dto)
    {
        return new Supervisor
        {
            Id = dto.Id,
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            EquipeId = dto.EquipeId
        };
    }
}