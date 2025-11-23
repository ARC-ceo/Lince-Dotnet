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
[SwaggerTag("Manipulação dos cadastros das cameras (CRUD). " +
            "Permite criar, atualizar, buscar e deletar os cadastros no sistema.")]
public class CameraController : ControllerBase
{
    private readonly ICameraUseCase _cameraUseCase;

    public CameraController(ICameraUseCase cameraUseCase)
    {
        _cameraUseCase = cameraUseCase;
    }
    
    [HttpPost]
    [SwaggerOperation(Summary = "Criar cadastro da camera", Description = "Adiciona um novo cadastro de camera na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.Created, "Cadastro criado com êxito",  typeof(CameraResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Setor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Camera>> Create(CameraDTO cameraDto)
    {
        try
        {
            var cameraCriado = await _cameraUseCase.CreateAsync(CameraDTO.ToEntity(cameraDto));
            return Created("", CameraResponse.ToResponse(cameraCriado));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpPut]
    [SwaggerOperation(Summary = "Atualizar cadastro da camera", Description = "Altera todo o cadastro da camera informada")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro atualizado com êxito",  typeof(CameraResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Camera e/ou Setor não encontrado")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Camera>> Update(CameraDTOUpdate cameraDtoUpdate)
    {
        try
        {
            var cameraAtualizado = await _cameraUseCase.UpdateAsync(CameraDTOUpdate.ToEntity(cameraDtoUpdate));
            return Ok(CameraResponse.ToResponse(cameraAtualizado));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Buscar cadastro da camera", Description = "Encontra o cadastro de uma camera especifica pelo ID")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Cadastro da camera é retornado",  typeof(CameraResponse))]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Camera não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Camera>> GetId(Guid id)
    {
        try
        {
            var camera = await _cameraUseCase.GetById(id);
            return Ok(CameraResponse.ToResponse(camera));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpGet]
    [SwaggerOperation(Summary = "Buscar todas cameras", Description = "Traz todos as cameras cadastradas atualmente na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de cameras retornada",  typeof(List<CameraResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Camera>> GetAll()
    {
        List<Camera> cameras = await _cameraUseCase.GetAllAsync();
        if (!cameras.Any())
        {
            return NoContent();
        }
        var camerasDto = cameras.Select(CameraResponse.ToResponse).ToList();
        return Ok(camerasDto);
    }
    
    [HttpGet("setor/{id}")]
    [SwaggerOperation(Summary = "Buscar todas cameras de um setor", Description = "Traz todas as cameras vinculados a um setor")]
    [SwaggerResponse((int)HttpStatusCode.OK, "Lista de cameras retornada",  typeof(List<CameraResponse>))]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult<Camera>> GetCamerasSetor(Guid id)
    {
        try
        {
            List<Camera> cameras = await _cameraUseCase.GetCamerasSetor(id);
            if (!cameras.Any())
            {
                return NoContent();
            }

            var camerasDto = cameras.Select(CameraResponse.ToResponse).ToList();
            return Ok(camerasDto);
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Deletar cadastro da camera", Description = "Apaga o cadastro de uma camera na aplicação")]
    [SwaggerResponse((int)HttpStatusCode.NoContent, "Sem conteúdo")]
    [SwaggerResponse((int)HttpStatusCode.BadRequest, "Erro na requisição")]
    [SwaggerResponse((int)HttpStatusCode.NotFound, "Camera não encontrada")]
    [SwaggerResponse((int)HttpStatusCode.Unauthorized, "Sem autorização")]
    [SwaggerResponse((int)HttpStatusCode.ServiceUnavailable, "Serviço indisponível")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await _cameraUseCase.DeleteAsync(id);
            return NoContent();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
    
}