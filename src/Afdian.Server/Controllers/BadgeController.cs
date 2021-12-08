using Afdian.Sdk;
using Afdian.Server.Configuration;
using Afdian.Server.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Afdian.Server.Models;
using System.Linq;

namespace Afdian.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BadgeController : ControllerBase
    {
        public AfdianConfiguration AfdianConfiguration { get; set; }

        private readonly ILogger<BadgeController> _logger;

        private readonly ApplicationDbContext _applicationDbContext;

        #region Ctor
        public BadgeController(IOptionsMonitor<AfdianConfiguration> afdianConfigurationOptionsMonitor, ApplicationDbContext applicationDbContext,
            ILogger<BadgeController> logger)
        {
            this.AfdianConfiguration = afdianConfigurationOptionsMonitor.CurrentValue;
            _applicationDbContext = applicationDbContext;
            _logger = logger;
        }
        #endregion

        #region Actions

        [Route("/badge.svg")]
        [HttpGet]
        public async Task<IActionResult> Badge([FromQuery] string badgeToken)
        {
            badgeToken = System.Net.WebUtility.UrlDecode(badgeToken);
            // 从 badgeToken 中解密出 userId, token
            string userIdAndToken = Sdk.Utils.AesUtil.DecryptEcbMode(badgeToken, this.AfdianConfiguration.BadgeEncrypt);
            string[] userIdAndTokenArr = userIdAndToken.Split(".", StringSplitOptions.RemoveEmptyEntries);

            if (userIdAndTokenArr == null || userIdAndTokenArr.Length != 2)
            {
                return NotFound();
            }

            return await Task.FromResult(MakeBadgeSvg(userIdAndTokenArr[0], userIdAndTokenArr[1]));
        }

        [Route("/{id}/badge.svg")]
        [HttpGet]
        public async Task<IActionResult> Badge([FromRoute] int id, [FromQuery] BadgeRequestModel badgeRequestModel)
        {
            // TODO: 根据 id 查询数据库
            Badge badge = _applicationDbContext.Badge?.FirstOrDefault(m => m.Id == id);
            if (badge == null)
            {
                return NotFound();
            }
            string userId = badge.UserId;
            string token = badge.Token;

            return await Task.FromResult(MakeBadgeSvg(userId, token));
        }

        [HttpPost]
        public async Task<IActionResult> Create(string userId, string token)
        {
            AfdianClient afdianClient = new AfdianClient(userId, token);
            var jsonStr = await afdianClient.PingAsync();
            if (!jsonStr.Contains("200"))
            {
                return Content("不合法的 user_id, token");
            }
            Badge badge = new Badge()
            {
                CreateTime = DateTime.Now,
                UserId = userId,
                Token = token
            };
            await _applicationDbContext.Badge.AddAsync(badge);
            await _applicationDbContext.SaveChangesAsync();

            return Content(badge.Id.ToString());
        }

        [Route("/{userId}/{token}/badge.svg")]
        [HttpGet]
        public async Task<IActionResult> Badge([FromRoute] string userId, [FromRoute] string token)
        {
            // 注意: 不要给 将 planId 放入参数列表, 否则变成必需项
            string planId = Request.Query["planId"];
            var contentResult = MakeBadgeSvg(userId, token, planId);

            return await Task.FromResult(contentResult);
        }


        [HttpGet, HttpPost]
        public async Task<IActionResult> BadgeToken(string userId, string token)
        {
            string str = $"{userId}.{token}";
            string badgeToken = Sdk.Utils.AesUtil.EncryptEcbMode(str, AfdianConfiguration.BadgeEncrypt);

            return await Task.FromResult(Content(badgeToken));
        }

        #endregion

        #region Helpers

        private ContentResult MakeBadgeSvg(string userId, string token, string planId = "")
        {
            AfdianClient afdianClient = new AfdianClient(userId, token);
            if (!string.IsNullOrEmpty(planId))
            {
                // TODO: 过滤 planId
            }
            var jsonModel = afdianClient.QueryOrderModel();
            int sponsorCount = jsonModel.data.total_count;
            int countTextLength = sponsorCount.ToString().Length * 65;

            string svgStr = System.IO.File.ReadAllText(@"Files/templates/badge.xml", System.Text.Encoding.UTF8);
            svgStr = svgStr.Replace("{{count}}", sponsorCount.ToString()).Replace("{{countTextLength}}", countTextLength.ToString());

            return Content(svgStr, "image/svg+xml;charset=utf-8");
        }

        #endregion





    }
}
