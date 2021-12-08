namespace Afdian.Server.Configuration
{
    public class AfdianConfiguration
    {
        public string VToken { get; set; }

        public string UserId { get; set; }

        public string Token { get; set; }

        /// <summary>
        /// 用于加密 UserId, Token 生成 badgeToken,
        /// 最后使用时, 服务端根据 badgeToken 解密出 UserId, Token
        /// </summary>
        public string BadgeEncrypt { get; set; }
    }
}
