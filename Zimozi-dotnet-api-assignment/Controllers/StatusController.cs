using Microsoft.AspNetCore.Mvc;

namespace Zimozi_dotnet_api_assignment.Controllers
{
    [ApiController]
    [Route("/status")]
    public class StatusController : Controller
    {
        [HttpGet]
        public ActionResult Status()
        {
            return Ok("API running");
        }
    }
}
