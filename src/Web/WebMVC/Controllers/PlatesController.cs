using Microsoft.AspNetCore.Mvc.Rendering;
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

    public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 20, string sortOrder = "RegistrationAsc")
    {
        var sortOptions = new List<SelectListItem>
        {
            new() { Value = "RegistrationAsc", Text = "Registration Ascending" },
            new() { Value = "RegistrationDesc", Text = "Registration Descending" },
            new() { Value = "SalePriceAsc", Text = "Sale Price Ascending" },
            new() { Value = "SalePriceDesc", Text = "Sale Price Descending" }
        };

        var response = await _httpClient
            .GetAsync($"http://catalog-api/api/plates?pageNumber={pageNumber}&pageSize={pageSize}&sortOrder={sortOrder}");
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
                    HasPreviousPage = paginatedResult.HasPreviousPage,
                    SortOrder = sortOrder,
                    SortOptions = sortOptions
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
