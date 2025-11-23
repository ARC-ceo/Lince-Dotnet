using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Lince.UseCase;

public class OperadorUseCase : IOperadorUseCase
{
    private readonly IOperadorRepository _operadorRepository;
    private readonly IEquipeRepository _equipeRepository;
    
    public OperadorUseCase(IOperadorRepository operadorRepository, IEquipeRepository equipeRepository)
    {
        _equipeRepository =  equipeRepository;
        _operadorRepository = operadorRepository;
    }
    
    public async Task<Operador> CreateAsync(Operador operador)
    {
        await GetEquipe(operador.EquipeId);
        await _operadorRepository.AddAsync(operador);
        return operador; 
    }

    public async Task<Operador> UpdateAsync(Operador operador)
    {
        var operadorExistente = await GetById(operador.Id);
        await GetEquipe(operador.EquipeId);
        
        operadorExistente.Nome = operador.Nome;
        operadorExistente.Funcao = operador.Funcao;
        operadorExistente.EquipeId = operador.EquipeId;
        
        await _operadorRepository.SaveChangesAsync();
        return operador; 
    }
    
    public async Task<Operador> GetById(Guid id)
    {
        var operador = await _operadorRepository.GetByIdAsync(id);
        if (operador == null)
            throw new NotFoundException("Operador não encontrado.");
        return operador;
    }
    
    public async Task GetEquipe(Guid id)
    {
        var equipe = await _equipeRepository.GetByIdAsync(id);
        if (equipe == null)
            throw new NotFoundException("Equipe não encontrada.");
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var operador = await GetById(id);
        await _operadorRepository.DeleteAsync(operador);
    }
    
    public async Task<List<Operador>> GetAllAsync()
    {
        return await _operadorRepository.GetAllAsync();
    }
    
    public async Task<List<Operador>> GetOperadoresEquipe(Guid equipeId)
    {
        await GetEquipe(equipeId);
        return await _operadorRepository.FindByEquipeIdAsync(equipeId);
    }
}