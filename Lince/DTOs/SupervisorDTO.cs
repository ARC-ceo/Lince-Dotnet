using System.ComponentModel.DataAnnotations;
using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.DTOs;

public class SupervisorDTO
{
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(200, MinimumLength = 2, ErrorMessage = "Informe um nome válido")]
    public required string Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório")]
    [EmailAddress(ErrorMessage = "Formato de email inválido")]
    [StringLength(200, ErrorMessage = "Informe um email válido")]
    public required string Email { get; set; }
        
    [RegularExpression(@"^\d{10,11}$", ErrorMessage = "O telefone deve ter 10 ou 11 dígitos")]
    public string? Telefone { get; set; }
    
    public Guid? EquipeId { get; set; }

    public static Supervisor ToEntity(SupervisorDTO dto)
    {
        return new Supervisor
        {
            Nome = dto.Nome,
            Email = dto.Email,
            Telefone = dto.Telefone,
            EquipeId = dto.EquipeId
        };
    }
}