using Backend.Features.Employees;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Ottiene la lista dei dipendenti.
    /// </summary>
    /// <param name="query">Parametri di query per filtrare i dipendenti.</param>
    /// <returns>Una lista di dipendenti.</returns>
    /// <response code="200">Restituisce la lista dei dipendenti.</response>
    /// <response code="400">Se la richiesta è malformata o non sono stati trovati risultati.</response>
    /// <response code="500">Se si verifica un errore interno del server.</response>
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<EmployeesListQueryResponse>>> GetEmployeesList(
        [FromQuery] EmployeesListQuery query)
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