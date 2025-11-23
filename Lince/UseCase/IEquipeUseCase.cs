using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.UseCase;

public interface IEquipeUseCase
{
    Task<Equipe> CreateAsync(Equipe equipe);
    Task<Equipe> UpdateAsync(Equipe equipe);
    Task<Equipe> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task<List<Equipe>> GetAllAsync();
}