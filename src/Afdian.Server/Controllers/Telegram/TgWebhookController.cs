using Afdian.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Afdian.Server.Controllers.Telegram
{
    //[Route("api/[controller]")]
    [ApiController]
    public class TgWebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromServices] HandleTgUpdateService handleUpdateService,
                                          [FromBody] Update update)
        {
            await handleUpdateService.EchoAsync(update);
            return Ok();
        }
    }
}
