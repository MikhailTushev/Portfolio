using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Web.Controllers
{
    [ApiController]
    [Route("[controller]/")]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}