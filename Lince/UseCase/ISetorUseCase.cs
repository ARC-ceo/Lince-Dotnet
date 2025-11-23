using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.UseCase;

public interface ISetorUseCase
{
    Task<Setor> CreateAsync(Setor setor);
    Task<Setor> UpdateAsync(Setor setor);
    Task<Setor> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task<List<Setor>> GetAllAsync();
}