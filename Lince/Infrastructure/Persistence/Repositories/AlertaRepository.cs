using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Lince.Infrastructure.Persistence.Repositories;

public class AlertaRepository(LinceContext context) : IAlertaRepository
{
    public async Task<Alerta> AddAsync(Alerta alerta)
    {
        context.Alertas.Add(alerta);
        await context.SaveChangesAsync();
        return alerta;
    }
    
    public async Task<Alerta?> GetByIdAsync(Guid id)
    {
        return await context.Alertas.FindAsync(id);
    }
    
    public async Task DeleteAsync(Alerta alerta)
    {
        context.Alertas.Remove(alerta);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Alerta>> FindByOperadorIdAsync(Guid operadorId)
    {
        return await context.Alertas
            .Where(a => a.OperadorId == operadorId)
            .ToListAsync();
    }
    
    public async Task<List<Alerta>> FindBySetorIdAsync(Guid setorId)
    {
        return await context.Alertas
            .Where(a => a.Camera.SetorId == setorId)
            .ToListAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Alerta>> GetAllAsync()
    {
        return await context.Alertas.ToListAsync();
    }
}