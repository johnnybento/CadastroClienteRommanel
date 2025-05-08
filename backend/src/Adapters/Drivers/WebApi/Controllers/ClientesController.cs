
using CadastroClienteRommanel.Application.UseCases.ObterClientePorId;
using CadastroClienteRommanel.Application.UseCases.RegistrarCliente;
using CadastroClienteRommanel.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CadastroClienteRommanel.Adapters.Drivers.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientesController(IMediator mediator)
        => _mediator = mediator;

    /// <summary>
    /// Registra um novo cliente.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegistrarClienteCommand cmd)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        try
        {
            var id = await _mediator.Send(cmd);
            return CreatedAtAction(nameof(GetById), new { id }, new { id });
        }
        catch (DomainException ex)
        {
            return BadRequest(new { message = ex.Message });
        }



       
    }

    /// <summary>
    /// Obtém um cliente por ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new ObterClientePorIdQuery(id);
        var dto = await _mediator.Send(query);
        if (dto is null)
            return NotFound();
        return Ok(dto);
    }
}