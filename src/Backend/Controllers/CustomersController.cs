using Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IExportService _exportService;

    public CustomersController(IMediator mediator, IExportService exportService)
    {
        _mediator = mediator;
        _exportService = exportService;
    }

    /// <summary>
    /// Ottiene la lista dei clienti.
    /// </summary>
    /// <param name="query">Parametri di query per filtrare i clienti.</param>
    /// <returns>Una lista di clienti.</returns>
    /// <response code="200">Restituisce la lista dei clienti.</response>
    /// <response code="400">Se la richiesta è malformata.</response>
    /// <response code="500">Se si verifica un errore interno del server.</response>
    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<CustomersListQueryResponse>>> GetCustomersList(
        [FromQuery] CustomersListQuery query)
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

    /// <summary>
    /// Esporta la lista dei clienti in formato XML.
    /// </summary>
    /// <returns>Un file XML contenente la lista dei clienti.</returns>
    /// <response code="200">Restituisce il file XML.</response>
    /// <response code="400">Se la richiesta è malformata.</response>
    /// <response code="500">Se si verifica un errore interno del server.</response>
    [HttpPost("export")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExportCustomers(CustomersListQuery customersListQuery)
    {
        var query = customersListQuery;
        var customers = await _mediator.Send(query);
        var stream = _exportService.ExportToXml(customers, "Customers");
        return File(stream, "application/xml", "customers.xml");
    }
}