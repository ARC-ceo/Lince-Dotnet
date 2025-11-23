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
[SwaggerTag("Manipulação dos cadastros dos alertas (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]
public class AlertaController : ControllerBase
{
    private readonly IAlertaUseCase _alertaUseCase;

    public AlertaController(IAlertaUseCase alertaUseCase)
    {
        _alertaUseCase = alertaUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro do alerta", Description = "Adiciona um novo cadastro de alerta na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(AlertaResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Camera e/ou Operador não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Alerta>> Create(AlertaDTO alertaDto)
    {
        try
        {
            var alertaCriado = await _alertaUseCase.CreateAsync(AlertaDTO.ToEntity(alertaDto));
            return Created("", AlertaResponse.ToResponse(alertaCriado));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro do alerta", Description = "Altera todo o cadastro do alerta informado")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(AlertaResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Camera e/ou Operador e/ou Alerta não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Alerta>> Update(AlertaDTOUpdate alertaDtoUpdate)
    {
        try
        {
            var alertaAtualizado = await _alertaUseCase.UpdateAsync(AlertaDTOUpdate.ToEntity(alertaDtoUpdate));
            return Ok(AlertaResponse.ToResponse(alertaAtualizado));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar cadastro do alerta", Description = "Encontra o cadastro de um alerta especifico pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro do alerta é retornado",  typeof(AlertaResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Alerta não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Alerta>> GetId(Guid id)
    {
        try
        {
            var alerta = await _alertaUseCase.GetById(id);
            return Ok(AlertaResponse.ToResponse(alerta));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todos alertas", Description = "Traz todos os alertas cadastrados atualmente na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de alertas retornada",  typeof(List<AlertaResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Alerta>> GetAll()
    {
        List<Alerta> alertas = await _alertaUseCase.GetAllAsync();
        if (!alertas.Any())
        {
            return NoContent();
        }
        var alertasDto = alertas.Select(AlertaResponse.ToResponse).ToList();
        return Ok(alertasDto);
    }
    
    [HttpGet("operador/{id}")]
    [SwaggerOperation(Summary = "Buscar todos alertas de um operador", Description = "Traz todos os alertas vinculados a um operador")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de alertas retornada",  typeof(List<AlertaResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Alerta>> GetAlertasOperador(Guid id)
    {
        try
        {
            List<Alerta> alertas = await _alertaUseCase.GetAlertaOperador(id);
            if (!alertas.Any())
            {
                return NoContent();
            }

            var alertasDto = alertas.Select(AlertaResponse.ToResponse).ToList();
            return Ok(alertasDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet("setor/{id}")]
    [SwaggerOperation(Summary = "Buscar todos alertas de um setor", Description = "Traz todos os alertas vinculados a um setor")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de alertas retornada",  typeof(List<AlertaResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Alerta>> GetAlertasSetor(Guid id)
    {
        try
        {
            List<Alerta> alertas = await _alertaUseCase.GetAlertaSetor(id);
            if (!alertas.Any())
            {
                return NoContent();
            }

            var alertasDto = alertas.Select(AlertaResponse.ToResponse).ToList();
            return Ok(alertasDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro do alerta", Description = "Apaga o cadastro de um alerta na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Alerta não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _alertaUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}