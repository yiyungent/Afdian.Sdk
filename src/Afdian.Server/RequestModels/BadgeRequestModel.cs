
using Microsoft.AspNetCore.Mvc;

namespace Afdian.Server.RequestModels
{
    /// <summary>
    /// 模型绑定: https://docs.microsoft.com/zh-cn/aspnet/core/mvc/models/model-binding?view=aspnetcore-6.0
    /// </summary>
    public class BadgeRequestModel
    {
        /// <summary>
        /// 过滤 方案ID
        /// </summary>
        public string planId { get; set; } = "";


    }
}
