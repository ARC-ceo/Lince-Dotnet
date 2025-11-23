using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lince.UseCase;

public class SetorUseCase : ISetorUseCase
{
    private readonly ISetorRepository _setorRepository;
    private readonly ICameraRepository _cameraRepository;
    
    public SetorUseCase(ISetorRepository setorRepository, ICameraRepository cameraRepository)
    {
        _setorRepository =  setorRepository;
        _cameraRepository = cameraRepository;
    }
    
    public async Task<Setor> CreateAsync(Setor setor)
    {
        if (await _setorRepository.FindByNomeAsync(setor.Nome) != null)
            throw new ConflitException("Já existe um setor com este nome.");
        try
        {
            await _setorRepository.AddAsync(setor);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new ConflitException("Já existe um setor com este nome.");
        }
        return setor; 
    }

    public async Task<Setor> UpdateAsync(Setor setor)
    {
        var setorExistente = await GetById(setor.Id);
        if (await _setorRepository.FindByNomeAsync(setor.Nome,setor.Id) != null)
            throw new ConflitException("Já existe um setor com este nome.");
 
        setorExistente.Nome = setor.Nome;
        setorExistente.Descricao = setor.Descricao;
        
        try
        {
            await _setorRepository.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)   
        {
            throw new ConflitException("Já existe um setor com este nome.");
        }
        return setor; 
    }
    
    public async Task<Setor> GetById(Guid id)
    {
        var setor = await _setorRepository.GetByIdAsync(id);
        if (setor == null)
            throw new NotFoundException("Setor não encontrado.");
        return setor;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var setor = await GetById(id);
        var cameras = await _cameraRepository.FindBySetorIdAsync(id);
        if (cameras.Any())
        {
            throw new ConflitException(
                "Não é possível deletar o setor pois existem cameras vinculados a ele."
            );
        }
        await _setorRepository.DeleteAsync(setor);
    }
    
    public async Task<List<Setor>> GetAllAsync()
    {
        return await _setorRepository.GetAllAsync();
    }
}