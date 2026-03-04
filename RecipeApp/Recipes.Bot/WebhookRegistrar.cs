using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Telegram.Bot;

namespace Recipes.Bot;

internal class WebhookRegistrar : IHostedService
{
  private readonly ITelegramBotClient _bot;
  private readonly TelegramOptions _options;

  public WebhookRegistrar(
    ITelegramBotClient bot,
    IOptions<TelegramOptions> options)
  {
    _bot = bot;
    _options = options.Value;
  }

  public async Task StartAsync(CancellationToken ct)
  {
    await _bot.SetWebhook(
      url: _options.WebhookUrl,
      cancellationToken: ct);

    Console.WriteLine($"Webhook registered: {_options.WebhookUrl}");
  }

  public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
}