using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        public PlatformsController() { }

        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            string message = "Inbound test of from Platforms Controller";
            Console.WriteLine(message);
            return Ok(message);
        }
    }
}