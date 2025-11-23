using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lince.UseCase;

public class SupervisorUseCase : ISupervisorUseCase
{
    private readonly ISupervisorRepository _supervisorRepository;
    private readonly IEquipeRepository _equipeRepository;
    
    public SupervisorUseCase(ISupervisorRepository supervisorRepository, IEquipeRepository equipeRepository)
    {
        _equipeRepository =  equipeRepository;
        _supervisorRepository = supervisorRepository;
    }
    
    public async Task<Supervisor> CreateAsync(Supervisor supervisor)
    {
        if (await _supervisorRepository.FindByEmailAsync(supervisor.Email) != null)
            throw new ConflitException("Email já cadastrado.");
        if (supervisor.Equipe is { Id: { } guid })
        {
            await GetEquipe(supervisor.Equipe.Id);
        }
        try
        {
            await _supervisorRepository.AddAsync(supervisor);
        }
        catch (DbUpdateException ex) 
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)
        {
            throw new ConflitException("Email já cadastrado");
        }
        return supervisor; 
    }

    public async Task<Supervisor> UpdateAsync(Supervisor supervisor)
    {
        var supervisorExistente = await GetById(supervisor.Id);
        if (await _supervisorRepository.FindByEmailAsync(supervisor.Email,supervisor.Id) != null)
            throw new ConflitException("Email já cadastrado");
        if (supervisor.Equipe is { Id: { } guid })
        {
            await GetEquipe(supervisor.Equipe.Id);
        }
        
        supervisorExistente.Nome = supervisor.Nome;
        supervisorExistente.Email = supervisor.Email;
        supervisorExistente.Telefone = supervisor.Telefone;
        supervisorExistente.EquipeId = supervisor.EquipeId;
        
        try
        {
            await _supervisorRepository.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
            when (ex.InnerException?.Message.Contains("UNIQUE") == true)   
        {
            throw new ConflitException("Email já cadastrado");
        }
        return supervisor; 
    }
    
    public async Task<Supervisor> GetById(Guid id)
    {
        var supervisor = await _supervisorRepository.GetByIdAsync(id);
        if (supervisor == null)
            throw new NotFoundException("Supervisor não encontrado.");
        return supervisor;
    }
    
    public async Task GetEquipe(Guid id)
    {
        var equipe = await _equipeRepository.GetByIdAsync(id);
        if (equipe == null)
            throw new NotFoundException("Equipe não encontrada.");
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var supervisor = await GetById(id);
        await _supervisorRepository.DeleteAsync(supervisor);
    }
    
    public async Task<List<Supervisor>> GetAllAsync()
    {
        return await _supervisorRepository.GetAllAsync();
    }
}