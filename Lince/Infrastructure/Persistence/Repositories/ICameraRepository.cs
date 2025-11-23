using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.Infrastructure.Persistence.Repositories;

public interface ICameraRepository
{
    Task<Camera> AddAsync(Camera camera);
    Task<Camera?> GetByIdAsync(Guid id);
    Task DeleteAsync(Camera camera);
    Task SaveChangesAsync();
    Task<List<Camera>> GetAllAsync();
    Task<List<Camera>> FindByStatusAsync(string status);
    Task<List<Camera>> FindBySetorIdAsync(Guid setorId);
}