using Microsoft.AspNetCore.Mvc;

namespace chat_api.Controllers
{
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public IActionResult Get()
        {
            return Redirect("/index.html");
        }
    }
}