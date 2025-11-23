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
[SwaggerTag("Manipulação dos cadastros dos setores (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]
public class SetorController : ControllerBase
{
    private readonly ISetorUseCase _setorUseCase;

    public SetorController(ISetorUseCase setorUseCase)
    {
        _setorUseCase = setorUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro do setor", Description = "Adiciona um novo setor na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(SetorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Setor>> Create(SetorDTO setorDto)
    {
        try
        {
            var setorCriado = await _setorUseCase.CreateAsync(SetorDTO.ToEntity(setorDto));
            return Created("", SetorResponse.ToResponse(setorCriado));
        }
        catch (ConflitException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro do setor", Description = "Altera todo o cadastro do setor informado")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(SetorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito nas informações de cadastro")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Setor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Setor>> Update(SetorDTOUpdate setorDtoUpdate)
    {
        try
        {
            var setorAtualizado = await _setorUseCase.UpdateAsync(SetorDTOUpdate.ToEntity(setorDtoUpdate));
            return Ok(SetorResponse.ToResponse(setorAtualizado));
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
    [SwaggerOperation(Summary = "Buscar cadastro do setor", Description = "Encontra o cadastro de um setor especifico pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro do setor é retornado",  typeof(SetorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Setor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Setor>> GetId(Guid id)
    {
        try
        {
            var setor = await _setorUseCase.GetById(id);
            return Ok(SetorResponse.ToResponse(setor));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todos os setores", Description = "Traz todos os setores cadastradas atualmente na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de setores retornada",  typeof(List<SetorResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Setor>> GetAll()
    {
        List<Setor> setores = await _setorUseCase.GetAllAsync();
        if (!setores.Any())
        {
            return NoContent();
        }
        var setoresDto = setores.Select(SetorResponse.ToResponse).ToList();
        return Ok(setoresDto);
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro do setor", Description = "Apaga o cadastro de um setor na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Setor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    [SwaggerResponse((int)HttpStatusCode.Conflict, "Conflito na solicitação")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _setorUseCase.DeleteAsync(id);
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