using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Afdian.Sdk
{
    public class AfdianClient
    {
        public string ApiBaseUrl { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }

        public AfdianClient(string userId, string token)
        {
            this.UserId = userId;
            this.Token = token;
            this.ApiBaseUrl = "https://afdian.net/api/open/";
        }

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

        public async Task<string> Ping()
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

        public async Task<string> QueryOrder(int page = 1)
        {



            return await Task.Run<string>(() => { return ""; });
        }


        public async Task<string> QuerySponsor(int page = 1)
        {



            return await Task.Run<string>(() => { return ""; });
        }



    }
}
