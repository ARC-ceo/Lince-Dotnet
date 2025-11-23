using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lince.UseCase;

public class EquipeUseCase : IEquipeUseCase
{
    private readonly IEquipeRepository _equipeRepository;
    private readonly ISupervisorRepository _supervisorRepository;
    private readonly IOperadorRepository _operadorRepository;
    
    public EquipeUseCase(IEquipeRepository equipeRepository, ISupervisorRepository supervisorRepository, IOperadorRepository operadorRepository)
    {
        _equipeRepository = equipeRepository;
        _supervisorRepository = supervisorRepository;
        _operadorRepository = operadorRepository;
    }
    
    public async Task<Equipe> CreateAsync(Equipe equipe)
    {
        if (await _equipeRepository.FindByNomeAsync(equipe.Nome) != null)
            throw new ConflitException("Já existe uma equipe com este nome.");
        try
        {
            await _equipeRepository.AddAsync(equipe);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new ConflitException("Já existe uma equipe com este nome.");
        }
        return equipe; 
    }

    public async Task<Equipe> UpdateAsync(Equipe equipe)
    {
        var equipeExistente = await GetById(equipe.Id);
        if (await _equipeRepository.FindByNomeAsync(equipe.Nome,equipe.Id) != null)
            throw new ConflitException("Já existe uma equipe com este nome.");
 
        equipeExistente.Nome = equipe.Nome;
        equipeExistente.Descricao = equipe.Descricao;
        
        try
        {
            await _equipeRepository.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)   
        {
            throw new ConflitException("Já existe uma equipe com este nome.");
        }
        return equipe; 
    }
    
    public async Task<Equipe> GetById(Guid id)
    {
        var equipe = await _equipeRepository.GetByIdAsync(id);
        if (equipe == null)
            throw new NotFoundException("Equipe não encontrada.");
        return equipe;
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var equipe = await GetById(id);
        var supervisores = await _supervisorRepository.FindByEquipeIdAsync(id);
        var operadores = await _operadorRepository.FindByEquipeIdAsync(id);
        if (supervisores.Any() | operadores.Any())
        {
            throw new ConflitException(
                "Não é possível deletar a equipe pois existem supervisores e/ou operadores vinculados a ela."
            );
        }
        await _equipeRepository.DeleteAsync(equipe);
    }
    
    public async Task<List<Equipe>> GetAllAsync()
    {
        return await _equipeRepository.GetAllAsync();
    }
}