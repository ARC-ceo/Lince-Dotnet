using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.UseCase;

public interface ICameraUseCase
{
    Task<Camera> CreateAsync(Camera camera);
    Task<Camera> UpdateAsync(Camera camera);
    Task<Camera> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task<List<Camera>> GetAllAsync();
    Task<List<Camera>> GetCamerasSetor(Guid setorId);
}