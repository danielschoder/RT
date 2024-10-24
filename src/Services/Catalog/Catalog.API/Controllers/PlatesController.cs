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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlateDto>>> GetPlates()
    {
        return Ok(await _platesManager.ListAsync());
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PlateDto>> GetPlate(Guid id)
    {
        var plate = await _platesManager.GetAsync(id);
        if (plate == null) { return NotFound(); }

        return Ok(plate);
    }
}
