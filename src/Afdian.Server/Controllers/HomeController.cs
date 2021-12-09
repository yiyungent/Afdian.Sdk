using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Afdian.Server.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<string> Version()
        {
            string version = Utils.CommonUtil.Version();

            return await Task.FromResult(version);
        }

    }
}
