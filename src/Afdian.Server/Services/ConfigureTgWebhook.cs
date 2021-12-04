using Afdian.Server.Configuration;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Afdian.Server.Services;

public class ConfigureTgWebhook : IHostedService
{
    private readonly ILogger<ConfigureTgWebhook> _logger;
    private readonly IServiceProvider _services;
    private readonly TgBotConfiguration _botConfig;

    public ConfigureTgWebhook(ILogger<ConfigureTgWebhook> logger,
                            IServiceProvider serviceProvider,
                            IConfiguration configuration)
    {
        _logger = logger;
        _services = serviceProvider;
        _botConfig = configuration.GetSection("TgBotConfiguration").Get<TgBotConfiguration>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        // Configure custom endpoint per Telegram API recommendations:
        // https://core.telegram.org/bots/api#setwebhook
        // If you'd like to make sure that the Webhook request comes from Telegram, we recommend
        // using a secret path in the URL, e.g. https://www.example.com/<token>.
        // Since nobody else knows your bot's token, you can be pretty sure it's us.
        var webhookAddress = @$"{_botConfig.HostAddress}/webhook/tgbot/{_botConfig.BotToken}";
        _logger.LogInformation("Setting webhook: {webhookAddress}", webhookAddress);
        await botClient.SetWebhookAsync(
            url: webhookAddress,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _services.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        // Remove webhook upon app shutdown
        _logger.LogInformation("Removing webhook");
        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}