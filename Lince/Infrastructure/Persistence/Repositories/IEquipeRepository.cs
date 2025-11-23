using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.Infrastructure.Persistence.Repositories;

public interface IEquipeRepository
{
    Task<Equipe> AddAsync(Equipe equipe);
    Task<Equipe?> GetByIdAsync(Guid id);
    Task DeleteAsync(Equipe equipe);
    Task SaveChangesAsync();
    Task<List<Equipe>> GetAllAsync();
    Task<Equipe?> FindByNomeAsync(string nome, Guid? idToIgnore = null);
}