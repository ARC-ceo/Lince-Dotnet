using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Lince.Infrastructure.Persistence.Repositories;

public class EquipeRepository(LinceContext context) : IEquipeRepository
{
    public async Task<Equipe> AddAsync(Equipe equipe)
    {
        context.Equipes.Add(equipe);
        await context.SaveChangesAsync();
        return equipe;
    }
    
    public async Task<Equipe?> GetByIdAsync(Guid id)
    {
        return await context.Equipes.FindAsync(id);
    }
    
    public async Task DeleteAsync(Equipe equipe)
    {
        context.Equipes.Remove(equipe);
        await context.SaveChangesAsync();
    }
    
    public async Task<Equipe?> FindByNomeAsync(string nome, Guid? idToIgnore = null)
    {
        return await context.Equipes
            .FirstOrDefaultAsync(c => c.Nome == nome && (idToIgnore == null || c.Id != idToIgnore.Value));
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Equipe>> GetAllAsync()
    {
        return await context.Equipes.ToListAsync();
    }
}