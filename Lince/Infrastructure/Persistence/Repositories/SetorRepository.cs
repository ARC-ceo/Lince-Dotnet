using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Lince.Infrastructure.Persistence.Repositories;

public class SetorRepository(LinceContext context) : ISetorRepository
{
    public async Task<Setor> AddAsync(Setor setor)
    {
        context.Setores.Add(setor);
        await context.SaveChangesAsync();
        return setor;
    }
    
    public async Task<Setor?> GetByIdAsync(Guid id)
    {
        return await context.Setores.FindAsync(id);
    }
    
    public async Task DeleteAsync(Setor setor)
    {
        context.Setores.Remove(setor);
        await context.SaveChangesAsync();
    }
    
    public async Task<Setor?> FindByNomeAsync(string nome, Guid? idToIgnore = null)
    {
        return await context.Setores
            .FirstOrDefaultAsync(c => c.Nome == nome && (idToIgnore == null || c.Id != idToIgnore.Value));
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Setor>> GetAllAsync()
    {
        return await context.Setores.ToListAsync();
    }
}