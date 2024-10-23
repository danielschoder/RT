namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("swagger")]
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }

        [HttpGet("plates")]
        public async Task<ActionResult<IEnumerable<Plate>>> GetPlates()
        {
            return Ok(await _dbContext.Plates.ToListAsync());
        }
    }
}
