using Backend.Features.Suppliers;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IExportService _exportService;

    public SuppliersController(IMediator mediator, IExportService exportService)
    {
        _mediator = mediator;
        _exportService = exportService;
    }

    /// <summary>
    /// Ottiene la lista dei fornitori.
    /// </summary>
    /// <param name="query">Parametri di query per filtrare i fornitori.</param>
    /// <returns>Una lista di fornitori.</returns>
    /// <response code="200">Restituisce la lista dei fornitori.</response>
    /// <response code="400">Se la richiesta è malformata o non sono stati trovati risultati.</response>
    /// <response code="500">Se si verifica un errore interno del server.</response>
    [HttpGet("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<List<SupplierListQueryResponse>>> GetSuppliersList([FromQuery] SupplierListQuery query)
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
    /// Esporta la lista dei fornitori in formato XML.
    /// </summary>
    /// <param name="supplierListQuery">Parametri di query per estrarre i fornitori filtrati.</param>
    /// <returns>Un file XML contenente la lista dei fornitori.</returns>
    /// <response code="200">Restituisce il file XML scaricabile.</response>
    /// <response code="400">Se la richiesta è malformata.</response>
    /// <response code="500">Se si verifica un errore interno del server.</response>
    [HttpPost("[action]")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> ExportSuppliers(SupplierListQuery supplierListQuery)
    {
        var query = supplierListQuery;
        var suppliers = await _mediator.Send(query);
        var stream = _exportService.ExportToXml(suppliers, "Suppliers");
        return File(stream, "application/xml", "suppliers.xml");
    }
}