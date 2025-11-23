using Lince.Infrastructure.Persistence.Entitites;

namespace Lince.UseCase;

public interface IAlertaUseCase
{
    Task<Alerta> CreateAsync(Alerta alerta);
    Task<Alerta> UpdateAsync(Alerta alerta);
    Task<Alerta> GetById(Guid id);
    Task DeleteAsync(Guid id);
    Task<List<Alerta>> GetAllAsync();
    Task<List<Alerta>> GetAlertaSetor(Guid setorId);
    Task<List<Alerta>> GetAlertaOperador(Guid operadorId);
}