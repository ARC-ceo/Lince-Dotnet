using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Lince.Infrastructure.Persistence.Repositories;

public class SupervisorRepository(LinceContext context) : ISupervisorRepository
{
    public async Task<Supervisor> AddAsync(Supervisor supervisor)
    {
        context.Supervisores.Add(supervisor);
        await context.SaveChangesAsync();
        return supervisor;
    }
    
    public async Task<Supervisor?> GetByIdAsync(Guid id)
    {
        return await context.Supervisores.FindAsync(id);
    }
    
    public async Task DeleteAsync(Supervisor supervisor)
    {
        context.Supervisores.Remove(supervisor);
        await context.SaveChangesAsync();
    }
    
    public async Task<Supervisor?> FindByEmailAsync(string email, Guid? idToIgnore = null)
    {
        return await context.Supervisores
            .FirstOrDefaultAsync(c => c.Email == email && (idToIgnore == null || c.Id != idToIgnore.Value));
    }
    
    public async Task<List<Supervisor>> FindByEquipeIdAsync(Guid equipeId)
    {
        return await context.Supervisores
            .Where(a => a.EquipeId == equipeId)
            .ToListAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Supervisor>> GetAllAsync()
    {
        return await context.Supervisores.ToListAsync();
    }

}