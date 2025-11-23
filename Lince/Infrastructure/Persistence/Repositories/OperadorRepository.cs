using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Lince.Infrastructure.Persistence.Repositories;

public class OperadorRepository(LinceContext context) : IOperadorRepository
{
    public async Task<Operador> AddAsync(Operador operador)
    {
        context.Operadores.Add(operador);
        await context.SaveChangesAsync();
        return operador;
    }
    
    public async Task<Operador?> GetByIdAsync(Guid id)
    {
        return await context.Operadores.FindAsync(id);
    }
    
    public async Task DeleteAsync(Operador operador)
    {
        context.Operadores.Remove(operador);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Operador>> FindByEquipeIdAsync(Guid equipeId)
    {
        return await context.Operadores
            .Where(a => a.EquipeId == equipeId)
            .ToListAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Operador>> GetAllAsync()
    {
        return await context.Operadores.ToListAsync();
    }
}