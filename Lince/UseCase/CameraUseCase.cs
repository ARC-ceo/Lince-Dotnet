using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;

namespace Lince.UseCase;

public class CameraUseCase : ICameraUseCase
{
    private readonly ICameraRepository _cameraRepository;
    private readonly ISetorRepository _setorRepository;
    
    public CameraUseCase(ICameraRepository cameraRepository, ISetorRepository setorRepository)
    {
        _cameraRepository =  cameraRepository;
        _setorRepository = setorRepository;
    }
    
    public async Task<Camera> CreateAsync(Camera camera)
    {
        await GetSetor(camera.SetorId);
        await _cameraRepository.AddAsync(camera);
        return camera; 
    }

    public async Task<Camera> UpdateAsync(Camera camera)
    {
        var cameraExistente = await GetById(camera.Id);
        await GetSetor(camera.SetorId);
        
        cameraExistente.Localizacao = camera.Localizacao;
        cameraExistente.Descricao = camera.Descricao;
        cameraExistente.Status = camera.Status;
        cameraExistente.SetorId = camera.SetorId;
        
        await _cameraRepository.SaveChangesAsync();
        return camera; 
    }
    
    public async Task<Camera> GetById(Guid id)
    {
        var camera = await _cameraRepository.GetByIdAsync(id);
        if (camera == null)
            throw new NotFoundException("Camera não encontrada.");
        return camera;
    }
    
    public async Task GetSetor(Guid id)
    {
        var setor = await _setorRepository.GetByIdAsync(id);
        if (setor == null)
            throw new NotFoundException("Setor não encontrado.");
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var camera = await GetById(id);
        await _cameraRepository.DeleteAsync(camera);
    }
    
    public async Task<List<Camera>> GetAllAsync()
    {
        return await _cameraRepository.GetAllAsync();
    }
    
    public async Task<List<Camera>> GetCamerasSetor(Guid setorId)
    {
        await GetSetor(setorId);
        return await _cameraRepository.FindBySetorIdAsync(setorId);
    }
    
}