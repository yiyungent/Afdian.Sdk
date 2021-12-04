using Afdian.Server.Configuration;
using Afdian.Server.RequestModels.Afdian;
using Afdian.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot.Types;

namespace Afdian.Server.Controllers.Afdian
{
    /// <summary>
    /// https://afdian.net/dashboard/dev
    /// Webhook URL（用来被动接收订单通知）
    /// </summary>
    //[Route("api/[controller]")]
    //[ApiController]
    public class AfdianWebhookController : ControllerBase
    {
        public AfdianConfiguration AfdianConfiguration { get; set; }

        public AfdianWebhookController(IOptionsMonitor<AfdianConfiguration> optionsMonitor)
        {
            this.AfdianConfiguration = optionsMonitor.CurrentValue;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AfdianWebhookRequestModel requestModel, [FromRoute] string vToken)
        {
            if (string.IsNullOrEmpty(vToken) || vToken != this.AfdianConfiguration.VToken)
            {
                return NotFound();
            }

            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine($"vToken: ", vToken);
            Console.WriteLine(jsonStr);

            return Ok();
        }



    }
}
