using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;

namespace Lince.UseCase;

public interface ISupervisorUseCase
{
    Task<Supervisor> CreateAsync(Supervisor supervisor);
    Task<Supervisor> UpdateAsync(Supervisor supervisor);
    Task<Supervisor> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task<List<Supervisor>> GetAllAsync();
}