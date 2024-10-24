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

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20)
    {
        var response = await _httpClient.GetAsync($"http://catalog-api/api/plates?pageNumber={pageNumber}&pageSize={pageSize}");
        if (response.IsSuccessStatusCode)
        {
            var paginatedResult = await response.Content.ReadFromJsonAsync<PaginatedResult<PlateBasicDto>>();
            if (paginatedResult != null)
            {
                var viewModel = new PaginatedPlatesViewModel
                {
                    Plates = paginatedResult.Data.ToList(),
                    CurrentPage = paginatedResult.CurrentPage,
                    PageSize = paginatedResult.PageSize,
                    TotalPages = paginatedResult.TotalPages,
                    HasNextPage = paginatedResult.HasNextPage,
                    HasPreviousPage = paginatedResult.HasPreviousPage
                };

                return View(viewModel);
            }
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
