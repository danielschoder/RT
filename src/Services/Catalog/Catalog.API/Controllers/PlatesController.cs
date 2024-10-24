using Catalog.API.BLL;
using Catalog.API.DTOs;

namespace Catalog.API.Controllers;

[ApiController]
[Route("api/plates")]
public class PlatesController : Controller
{
    private readonly IPlatesManager _platesManager;

    public PlatesController(IPlatesManager platesManager)
    {
        _platesManager = platesManager;
    }

    [HttpPost]
    public async Task<ActionResult<PlateDto>> CreatePlate([FromBody] PlateDto newPlate)
    {
        var createdPlate = await _platesManager.CreateAsync(newPlate);

        return CreatedAtAction(nameof(GetPlate), new { id = createdPlate.Id }, createdPlate);
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResult<PlateDto>>> GetPlates(
        int pageNumber = 1,
        int pageSize = 20,
        string sortOrder = "RegistrationAsc")
    {
        var result = await _platesManager.ListAsync(pageNumber, pageSize, sortOrder);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlateDto>> GetPlate(Guid id)
    {
        var plate = await _platesManager.GetAsync(id);
        return plate is null ? NotFound() : Ok(plate);
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdatePlateStatus(Guid id, [FromBody] int status)
    {
        var plate = await _platesManager.UpdateStatusAsync(id, status);
        return plate is null ? NotFound() : Ok(plate);
    }
}
