using Lince.Infrastructure.Persistence.Entitites;
using Microsoft.EntityFrameworkCore;

namespace Lince.Infrastructure.Persistence.Repositories;

public class CameraRepository(LinceContext context) : ICameraRepository
{
    public async Task<Camera> AddAsync(Camera camera)
    {
        context.Cameras.Add(camera);
        await context.SaveChangesAsync();
        return camera;
    }
    
    public async Task<Camera?> GetByIdAsync(Guid id)
    {
        return await context.Cameras.FindAsync(id);
    }
    
    public async Task DeleteAsync(Camera camera)
    {
        context.Cameras.Remove(camera);
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Camera>> FindByStatusAsync(string status)
    {
        return await context.Cameras
            .Where(a => a.Status == status)
            .ToListAsync();
    }
    
    public async Task<List<Camera>> FindBySetorIdAsync(Guid setorId)
    {
        return await context.Cameras
            .Where(a => a.SetorId == setorId)
            .ToListAsync();
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
    
    public async Task<List<Camera>> GetAllAsync()
    {
        return await context.Cameras.ToListAsync();
    }
}