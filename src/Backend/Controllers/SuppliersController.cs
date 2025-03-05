using Backend.Features.Suppliers;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly IMediator _mediator;

    public SuppliersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Ottiene la lista dei fornitori.
    /// </summary>
    /// <param name="query">Parametri di query per filtrare i fornitori.</param>
    /// <returns>Una lista di fornitori.</returns>
    /// <response code="200">Restituisce la lista dei fornitori.</response>
    /// <response code="400">Se la richiesta è malformata o non sono stati trovati risultati.</response>
    /// <response code="500">Se si verifica un errore interno del server.</response>
    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SupplierListQueryResponse>>> GetSuppliersList(
        [FromQuery] SupplierListQuery query)
    {
        try
        {
            var result = await _mediator.Send(query);
            if(result == null || !result.Any())
            {
                return BadRequest("Nessun valore trovato");
            }
            else
            {
                return Ok(result);
            }
            
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}