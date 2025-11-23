using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;

namespace Lince.UseCase;

public class AlertaUseCase : IAlertaUseCase
{
    private readonly IAlertaRepository _alertaRepository;
    private readonly ICameraRepository _cameraRepository;
    private readonly ISetorRepository _setorRepository;
    private readonly IOperadorRepository _operadorRepository;
    
    public AlertaUseCase(IAlertaRepository alertaRepository,ICameraRepository cameraRepository, ISetorRepository setorRepository, IOperadorRepository operadorRepository)
    {
        _alertaRepository =  alertaRepository;
        _cameraRepository = cameraRepository;
        _setorRepository = setorRepository;
        _operadorRepository = operadorRepository;
        
    }
    
    public async Task<Alerta> CreateAsync(Alerta alerta)
    {
        await GetCamera(alerta.CameraId);
        await GetOperador(alerta.OperadorId);
        await _alertaRepository.AddAsync(alerta);
        return alerta; 
    }

    public async Task<Alerta> UpdateAsync(Alerta alerta)
    {
        var alertaExistente = await GetById(alerta.Id);
        await GetCamera(alerta.CameraId);
        await GetOperador(alerta.OperadorId);
        
        alertaExistente.Motivo = alerta.Motivo;
        alertaExistente.NivelAlerta = alerta.NivelAlerta;
        alertaExistente.CameraId = alerta.CameraId;
        alertaExistente.OperadorId = alerta.OperadorId;
        
        await _alertaRepository.SaveChangesAsync();
        return alerta; 
    }
    
    public async Task<Alerta> GetById(Guid id)
    {
        var alerta = await _alertaRepository.GetByIdAsync(id);
        if (alerta == null)
            throw new NotFoundException("Alerta não encontrado.");
        return alerta;
    }
    
    public async Task GetCamera(Guid id)
    {
        var camera = await _cameraRepository.GetByIdAsync(id);
        if (camera == null)
            throw new NotFoundException("Camera não encontrada.");
    }
    
    public async Task GetOperador(Guid id)
    {
        var operador = await _operadorRepository.GetByIdAsync(id);
        if (operador == null)
            throw new NotFoundException("Operador não encontrado.");
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var alerta = await GetById(id);
        await _alertaRepository.DeleteAsync(alerta);
    }
    
    public async Task<List<Alerta>> GetAllAsync()
    {
        return await _alertaRepository.GetAllAsync();
    }
    
    public async Task<List<Alerta>> GetAlertaSetor(Guid setorId)
    {
        var setor = await _setorRepository.GetByIdAsync(setorId);
        if (setor == null)
            throw new NotFoundException("Setor não encontrado.");
        return await _alertaRepository.FindBySetorIdAsync(setorId);
    }
    
    public async Task<List<Alerta>> GetAlertaOperador(Guid operadorId)
    {
        await GetOperador(operadorId);
        return await _alertaRepository.FindByOperadorIdAsync(operadorId);
    }
}