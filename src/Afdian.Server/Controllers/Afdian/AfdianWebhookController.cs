using Afdian.Server.Configuration;
using Afdian.Server.RequestModels.Afdian;
using Afdian.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Telegram.Bot;
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
        public AfdianConfiguration AfdianConfiguration { get; }

        public TgBotConfiguration TgBotConfiguration { get; }

        private readonly ITelegramBotClient _botClient;

        public AfdianWebhookController(IOptionsMonitor<AfdianConfiguration> afdianOptionsMonitor,
            IOptionsMonitor<TgBotConfiguration> tgOptionsMonitor,
            ITelegramBotClient botClient)
        {
            this.AfdianConfiguration = afdianOptionsMonitor.CurrentValue;
            this.TgBotConfiguration = tgOptionsMonitor.CurrentValue;
            this._botClient = botClient;
        }

        [HttpPost, HttpGet]
        public async Task<IActionResult> Post(AfdianWebhookRequestModel requestModel)
        {
            string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(requestModel);
            Console.WriteLine(DateTime.Now.ToString());
            Console.WriteLine(jsonStr);

            if (TgBotConfiguration.AdminChatId != 0 && requestModel.data != null)
            {
                string productType = "常规方案 (订阅)";
                switch (requestModel.data.order.product_type)
                {
                    case 0:
                        productType = "常规方案 (订阅)";
                        break;
                    case 1:
                        productType = "售卖方案";
                        break;
                    default:
                        break;
                }

                await _botClient.SendTextMessageAsync(
                        chatId: this.TgBotConfiguration.AdminChatId,
                        text: $"订单号: {requestModel.data.order.user_id} \n" +
                              $"下单用户ID: {requestModel.data.order.out_trade_no} \n" +
                              $"方案ID: {requestModel.data.order.plan_id} \n" +
                              $"赞助月份： {requestModel.data.order.month} \n" +
                              $"真实付款金额： {requestModel.data.order.total_amount} \n" +
                              $"显示金额: {requestModel.data.order.show_amount} \n" +
                              $"订单留言: {requestModel.data.order.remark} \n" +
                              $"折扣: {requestModel.data.order.discount} \n" +
                              $"类型: {productType} \n");
            }


            return Ok(new
            {
                ec = 200,
                em = ""
            });
        }



    }
}
