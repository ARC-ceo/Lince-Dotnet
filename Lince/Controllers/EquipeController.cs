using System.Net;
using Lince.DTOs;
using Lince.Exceptions;
using Lince.Infrastructure.Persistence.Entitites;
using Lince.UseCase;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Lince.Controllers;

[ApiController]
[Route("[controller]")]
[SwaggerTag("Manipulação dos cadastros das equipes (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]
public class EquipeController : ControllerBase
{
    private readonly IEquipeUseCase _equipeUseCase;

    public EquipeController(IEquipeUseCase equipeUseCase)
    {
        _equipeUseCase = equipeUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro da equipe", Description = "Adiciona uma nova equipe na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(EquipeResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Equipe>> Create(EquipeDTO equipeDto)
    {
        try
        {
            var equipeCriado = await _equipeUseCase.CreateAsync(EquipeDTO.ToEntity(equipeDto));
            return Created("", EquipeResponse.ToResponse(equipeCriado));
        }
        catch (ConflitException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro da equipe", Description = "Altera todo o cadastro da equipe informada")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(EquipeResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Equipe não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Equipe>> Update(EquipeDTOUpdate equipeDtoUpdate)
    {
        try
        {
            var equipeAtualizado = await _equipeUseCase.UpdateAsync(EquipeDTOUpdate.ToEntity(equipeDtoUpdate));
            return Ok(EquipeResponse.ToResponse(equipeAtualizado));
        }
        catch (ConflitException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar cadastro da equipe", Description = "Encontra o cadastro de uma equipe especifica pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro da equipe é retornado",  typeof(EquipeResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Equipe não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Equipe>> GetId(Guid id)
    {
        try
        {
            var equipe = await _equipeUseCase.GetById(id);
            return Ok(EquipeResponse.ToResponse(equipe));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todas as equipes", Description = "Traz todas as equipes cadastradas atualmente na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de equipes retornada",  typeof(List<EquipeResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Equipe>> GetAll()
    {
        List<Equipe> equipes = await _equipeUseCase.GetAllAsync();
        if (!equipes.Any())
        {
            return NoContent();
        }
        var equipesDto = equipes.Select(EquipeResponse.ToResponse).ToList();
        return Ok(equipesDto);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro da equipe", Description = "Apaga o cadastro de uma equipe na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Equipe não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito na solicitação")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _equipeUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ConflitException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
}