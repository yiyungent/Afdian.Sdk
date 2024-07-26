using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afdian.Sdk
{
    public class AfdianClient
    {
        #region Props
        public string ApiBaseUrl { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }
        #endregion

        #region Ctor
        public AfdianClient(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
            this.ApiBaseUrl = "https://afdian.com/api/open/";
        }
        #endregion

        #region Methods

        /// <summary>
        /// sign = md5({token}{将参数key排序后，拼接value})  token 为当前页面上生成的 token，注意拼接时，不需要带上"token"字符串
        /// 
        /// 具体为
        /// sign = md5({token}params{params}ts{ts}user_id{user_id})
        /// </summary>
        /// <returns></returns>
        public string Sign(string paramsStr)
        {
            // ts 为 10位时间戳
            long ts = Utils.DateTimeUtil.NowTimeStamp10();
            string sign = Utils.Md5Util.MD5Encrypt32($"{this.Token}params{paramsStr}ts{ts}user_id{this.UserId}");

            return sign;
        }

        public async Task<string> PingAsync()
        {
            string url = $"{this.ApiBaseUrl}ping";

            long ts = Utils.DateTimeUtil.NowTimeStamp10();
            string paramsStr = Utils.JsonUtil.Obj2JsonStr(new { page = 1 });
            string bodyJsonStr = Utils.JsonUtil.Obj2JsonStr(new RequestModels.CommonRequestModel
            {
                user_id = this.UserId,
                ts = ts,
                @params = paramsStr,
                sign = Sign(paramsStr)
            });

            StringBuilder responseHeadersSb = new StringBuilder();

            return await Task.Run<string>(() =>
            {
                return Utils.HttpUtil.HttpPost(url: url, postDataStr: bodyJsonStr, responseHeadersSb: responseHeadersSb, headers: new string[] {
                    "content-type: application/json"
                });
            });
        }

        public string Ping()
        {
            return PingAsync().Result;
        }

        public async Task<string> QueryOrderAsync(int page = 1)
        {
            string url = $"{this.ApiBaseUrl}query-order";

            long ts = Utils.DateTimeUtil.NowTimeStamp10();
            string paramsStr = Utils.JsonUtil.Obj2JsonStr(new { page = page });
            string bodyJsonStr = Utils.JsonUtil.Obj2JsonStr(new RequestModels.CommonRequestModel
            {
                user_id = this.UserId,
                ts = ts,
                @params = paramsStr,
                sign = Sign(paramsStr)
            });

            StringBuilder responseHeadersSb = new StringBuilder();

            return await Task.Run<string>(() =>
            {
                return Utils.HttpUtil.HttpPost(url: url, postDataStr: bodyJsonStr, responseHeadersSb: responseHeadersSb, headers: new string[] {
                    "content-type: application/json"
                });
            });
        }

        public string QueryOrder(int page = 1)
        {
            return QueryOrderAsync(page).Result;
        }

        public async Task<ResponseModels.QueryOrderResponseModel> QueryOrderModelAsync(int page = 1)
        {
            string jsonStr = await QueryOrderAsync(page);
            ResponseModels.QueryOrderResponseModel responseModel = Utils.JsonUtil.JsonStr2Obj<ResponseModels.QueryOrderResponseModel>(jsonStr);

            return responseModel;
        }

        public ResponseModels.QueryOrderResponseModel QueryOrderModel(int page = 1)
        {
            return QueryOrderModelAsync(page).Result;
        }

        public async Task<string> QuerySponsorAsync(int page = 1)
        {
            string url = $"{this.ApiBaseUrl}query-sponsor";

            long ts = Utils.DateTimeUtil.NowTimeStamp10();
            string paramsStr = Utils.JsonUtil.Obj2JsonStr(new { page = page });
            string bodyJsonStr = Utils.JsonUtil.Obj2JsonStr(new RequestModels.CommonRequestModel
            {
                user_id = this.UserId,
                ts = ts,
                @params = paramsStr,
                sign = Sign(paramsStr)
            });

            StringBuilder responseHeadersSb = new StringBuilder();

            return await Task.Run<string>(() =>
            {
                return Utils.HttpUtil.HttpPost(url: url, postDataStr: bodyJsonStr, responseHeadersSb: responseHeadersSb, headers: new string[] {
                    "content-type: application/json"
                });
            });
        }

        public string QuerySponsor(int page = 1)
        {
            return QuerySponsorAsync(page).Result;
        }

        public async Task<ResponseModels.QuerySponsorResponseModel> QuerySponsorModelAsync(int page = 1)
        {
            string jsonStr = await QuerySponsorAsync(page);
            ResponseModels.QuerySponsorResponseModel responseModel = Utils.JsonUtil.JsonStr2Obj<ResponseModels.QuerySponsorResponseModel>(jsonStr);

            return responseModel;
        }

        public ResponseModels.QuerySponsorResponseModel QuerySponsorModel(int page = 1)
        {
            return QuerySponsorModelAsync(page).Result;
        }

        #endregion



    }
}
