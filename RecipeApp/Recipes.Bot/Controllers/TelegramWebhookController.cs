using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Recipes.Bot.Controllers;

[ApiController]
[Route("telegram/webhook")]
public class TelegramWebhookController : ControllerBase
{
  private readonly ITelegramBotClient _bot;
  private readonly Recipe.Recipe.RecipeClient _recipeClient;

  public TelegramWebhookController(
    ITelegramBotClient bot,
    Recipe.Recipe.RecipeClient recipeClient)
  {
    _bot = bot;
    _recipeClient = recipeClient;
  }

  [HttpPost]
  public async Task<IActionResult> Post([FromBody] Update update)
  {
    if (update.Message?.Text is not { } text)
      return Ok();

    var reply = await _recipeClient.PingAsync(new Empty());

    await _bot.SendMessage(chatId: update.Message.Chat.Id,
      text: $"Hello 👋");

    return Ok();
  }
}