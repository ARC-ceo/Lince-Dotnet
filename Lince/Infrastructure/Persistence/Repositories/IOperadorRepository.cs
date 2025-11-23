using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.Infrastructure.Persistence.Repositories;

public interface IOperadorRepository
{
    Task<Operador> AddAsync(Operador operador);
    Task<Operador?> GetByIdAsync(Guid id);
    Task DeleteAsync(Operador operador);
    Task SaveChangesAsync();
    Task<List<Operador>> GetAllAsync();
    Task<List<Operador>> FindByEquipeIdAsync(Guid equipeId);
}