using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.UseCase;

public interface IOperadorUseCase
{
    Task<Operador> CreateAsync(Operador operador);
    Task<Operador> UpdateAsync(Operador operador);
    Task<Operador> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task<List<Operador>> GetAllAsync();
    Task<List<Operador>> GetOperadoresEquipe(Guid equipeId);
}