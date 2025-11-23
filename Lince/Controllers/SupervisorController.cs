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
[SwaggerTag("Manipulação dos cadastros dos supervisores (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]
public class SupervisorController : ControllerBase
{
    private readonly ISupervisorUseCase _supervisorUseCase;

    public SupervisorController(ISupervisorUseCase supervisorUseCase)
    {
        _supervisorUseCase = supervisorUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro do supervisor", Description = "Adiciona um novo cadastro de supervisor na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(SupervisorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Supervisor>> Create(SupervisorDTO supervisorDto)
    {
        try
        {
            var supervisorCriado = await _supervisorUseCase.CreateAsync(SupervisorDTO.ToEntity(supervisorDto));
            return Created("", SupervisorResponse.ToResponse(supervisorCriado));
        }
        catch (ConflitException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro do supervisor", Description = "Altera todo o cadastro do supervisor informado")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(SupervisorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Cliente não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Supervisor>> Update(SupervisorDTOUpdate supervisorDtoUpdate)
    {
        try
        {
            var supervisorAtualizado = await _supervisorUseCase.UpdateAsync(SupervisorDTOUpdate.ToEntity(supervisorDtoUpdate));
            return Ok(SupervisorResponse.ToResponse(supervisorAtualizado));
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
    [SwaggerOperation(Summary = "Buscar cadastro do supervisor", Description = "Encontra o cadastro de um supervisor especifico pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro do supervisor é retornado",  typeof(SupervisorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Cliente não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Supervisor>> GetId(Guid id)
    {
        try
        {
            var supervisor = await _supervisorUseCase.GetById(id);
            return Ok(SupervisorResponse.ToResponse(supervisor));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todos supervisores", Description = "Traz todos os supervisores cadastrados atualmente na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de supervisores retornada",  typeof(List<SupervisorResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Supervisor>> GetAll()
    {
        List<Supervisor> supervisores = await _supervisorUseCase.GetAllAsync();
        if (!supervisores.Any())
        {
            return NoContent();
        }
        var supervisoresDto = supervisores.Select(SupervisorResponse.ToResponse).ToList();
        return Ok(supervisoresDto);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro do supervisor", Description = "Apaga o cadastro de um supervisor na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Supervisor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _supervisorUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
}