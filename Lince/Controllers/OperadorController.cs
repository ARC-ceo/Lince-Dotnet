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
[SwaggerTag("Manipulação dos cadastros dos operadores (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]
public class OperadorController : ControllerBase
{
    private readonly IOperadorUseCase _operadorUseCase;

    public OperadorController(IOperadorUseCase operadorUseCase)
    {
        _operadorUseCase = operadorUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro do operador", Description = "Adiciona um novo cadastro de operador na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(OperadorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Equipe não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Operador>> Create(OperadorDTO operadorDto)
    {
        try
        {
            var operadorCriado = await _operadorUseCase.CreateAsync(OperadorDTO.ToEntity(operadorDto));
            return Created("", OperadorResponse.ToResponse(operadorCriado));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro do operador", Description = "Altera todo o cadastro do operador informado")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(OperadorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Operador e/ou Equipe não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Operador>> Update(OperadorDTOUpdate operadorDtoUpdate)
    {
        try
        {
            var operadorAtualizado = await _operadorUseCase.UpdateAsync(OperadorDTOUpdate.ToEntity(operadorDtoUpdate));
            return Ok(OperadorResponse.ToResponse(operadorAtualizado));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar cadastro do operador", Description = "Encontra o cadastro de um operador especifico pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro do operador é retornado",  typeof(OperadorResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Operador não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Operador>> GetId(Guid id)
    {
        try
        {
            var operador = await _operadorUseCase.GetById(id);
            return Ok(OperadorResponse.ToResponse(operador));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todos operadores", Description = "Traz todos os operadores cadastrados atualmente na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de operadores retornada",  typeof(List<OperadorResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Operador>> GetAll()
    {
        List<Operador> operadores = await _operadorUseCase.GetAllAsync();
        if (!operadores.Any())
        {
            return NoContent();
        }
        var operadoresDto = operadores.Select(OperadorResponse.ToResponse).ToList();
        return Ok(operadoresDto);
    }
    
    [HttpGet("equipe/{id}")]
    [SwaggerOperation(Summary = "Buscar todos operadores de uma equipe", Description = "Traz todos os operadores vinculados a uma equipe")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de operadores retornada",  typeof(List<OperadorResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Operador>> getOperadoresEquipe(Guid id)
    {
        try
        {
            List<Operador> operadores = await _operadorUseCase.GetOperadoresEquipe(id);
            if (!operadores.Any())
            {
                return NoContent();
            }

            var operadoresDto = operadores.Select(OperadorResponse.ToResponse).ToList();
            return Ok(operadoresDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro do operador", Description = "Apaga o cadastro de um operador na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Operador não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _operadorUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}