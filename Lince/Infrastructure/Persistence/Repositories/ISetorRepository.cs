using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.Infrastructure.Persistence.Repositories;

public interface ISetorRepository
{
    Task<Setor> AddAsync(Setor setor);
    Task<Setor?> GetByIdAsync(Guid id);
    Task DeleteAsync(Setor setor);
    Task SaveChangesAsync();
    Task<List<Setor>> GetAllAsync();
    Task<Setor?> FindByNomeAsync(string nome, Guid? idToIgnore = null);
}