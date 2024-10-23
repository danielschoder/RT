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
        return Ok(await _platesManager.GetPlatesAsync());
    }
}
