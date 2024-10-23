namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpGet("swagger")]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

        [HttpGet("plates")]
        public ActionResult<IEnumerable<Plate>> GetPlates()
        {
            var plates = new List<Plate>
            {
                new Plate
                {
                    Id = Guid.NewGuid(),
                    Registration = "ABC123",
                    PurchasePrice = 100.00m,
                    SalePrice = 150.00m,
                    Letters = "ABC",
                    Numbers = 123
                }
            };

            return Ok(plates);
        }
    }
}
