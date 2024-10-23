using RTCodingExercise.Microservices.Models;
using System.Diagnostics;
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
        try
        {
            var response = await _httpClient.GetAsync("http://catalog-api/api/plates");
            if (response.IsSuccessStatusCode)
            {
                var plates = await response.Content.ReadFromJsonAsync<List<PlateBasicDto>>();
                return View(new PlatesViewModel { Plates = plates });
            }
        }
        catch (Exception ex)
        {
            var x = ex.Message;
        }

        return BadRequest("Failed to fetch data.");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
