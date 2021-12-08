using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Afdian.Server.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        [Route("")]
        [HttpGet]
        public ActionResult Index()
        {
            return File("index.html", "text/html");
        }
    }
}
