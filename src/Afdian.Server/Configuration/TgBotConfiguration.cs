namespace Afdian.Server.Configuration
{
    public class TgBotConfiguration
    {
        public string BotToken { get; init; }
        public string HostAddress { get; init; }

        public long AdminChatId { get; set; }
    }
}
