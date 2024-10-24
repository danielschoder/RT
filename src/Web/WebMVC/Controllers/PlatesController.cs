using WebMVC.DTOs;
using WebMVC.Models;

namespace WebMVC.Controllers;

public class PlatesController : Controller
{
    private readonly ILogger<PlatesController> _logger;
    private readonly HttpClient _httpClient;

    public PlatesController(
        ILogger<PlatesController> logger,
        HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _httpClient.GetAsync("http://catalog-api/api/plates");
        if (response.IsSuccessStatusCode)
        {
            var plates = (await response.Content.ReadFromJsonAsync<List<PlateBasicDto>>());
            return View(new PlatesViewModel { Plates = plates });
        }

        return BadRequest("Failed to fetch data.");
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var response = await _httpClient.GetAsync($"http://catalog-api/api/plates/{id}");
        if (response.IsSuccessStatusCode)
        {
            var plate = await response.Content.ReadFromJsonAsync<PlateDto>();
            return View(plate);
        }

        return NotFound();
    }
}
