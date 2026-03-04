using Telegram.Bot;
using Telegram.Bot.Polling;

namespace RecipeManagerBot.Services;

public class ReceiverService : BackgroundService
{
    private readonly ITelegramBotClient _botClient;
    private readonly UpdateHandler _updateHandler;

    public ReceiverService(ITelegramBotClient botClient, UpdateHandler updateHandler)
    {
        _botClient = botClient;
        _updateHandler = updateHandler;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var options = new ReceiverOptions { AllowedUpdates = [] }; // Слушать всё

        _botClient.StartReceiving(
            updateHandler: (bot, update, ct) => _updateHandler.HandleUpdateAsync(update, ct),
            errorHandler: (bot, ex, ct) => _updateHandler.HandlePollingErrorAsync(bot, ex, ct),
            receiverOptions: options,
            cancellationToken: stoppingToken
        );

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }
}