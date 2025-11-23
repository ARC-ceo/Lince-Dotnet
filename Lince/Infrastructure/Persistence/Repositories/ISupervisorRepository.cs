using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.Infrastructure.Persistence.Repositories;

public interface ISupervisorRepository
{
    Task<Supervisor> AddAsync(Supervisor supervisor);
    Task<Supervisor?> GetByIdAsync(Guid id);
    Task DeleteAsync(Supervisor supervisor);
    Task SaveChangesAsync();
    Task<List<Supervisor>> GetAllAsync();
    Task<Supervisor?> FindByEmailAsync(string email, Guid? idToIgnore = null);
    Task<List<Supervisor>> FindByEquipeIdAsync(Guid equipeId);
}