using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.Infrastructure.Persistence.Repositories;

public interface IAlertaRepository
{
    Task<Alerta> AddAsync(Alerta alerta);
    Task<Alerta?> GetByIdAsync(Guid id);
    Task DeleteAsync(Alerta alerta);
    Task SaveChangesAsync();
    Task<List<Alerta>> GetAllAsync();
    Task<List<Alerta>> FindByOperadorIdAsync(Guid operadorId);
    Task<List<Alerta>> FindBySetorIdAsync(Guid setorId);
}