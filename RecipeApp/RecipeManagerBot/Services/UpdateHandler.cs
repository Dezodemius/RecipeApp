using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using RecipeManagerBot.Data;
using RecipeManagerBot.Models;
using Microsoft.EntityFrameworkCore;

namespace RecipeManagerBot.Services;

public class UpdateHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IServiceScopeFactory _scopeFactory;

    public UpdateHandler(ITelegramBotClient botClient, IServiceScopeFactory scopeFactory)
    {
        _botClient = botClient;
        _scopeFactory = scopeFactory;
    }

    public async Task HandleUpdateAsync(Update update, CancellationToken ct)
    {
        // Нас интересуют только текстовые сообщения
        if (update.Message is not { Text: { } messageText } message) return;

        var chatId = message.Chat.Id;

        if (messageText == "/start")
        {
            await HandleStartCommand(message, ct);
        }
    }

    private async Task HandleStartCommand(Message message, CancellationToken ct)
    {
        using var scope = _scopeFactory.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        // 1. Регистрируем или обновляем пользователя в БД
        var user = await db.Users.FirstOrDefaultAsync(u => u.TelegramId == message.From!.Id, ct);
        if (user == null)
        {
            user = new Models.User 
            { 
                TelegramId = message.From!.Id, 
                Username = message.From.Username ?? "Anonymous" 
            };
            db.Users.Add(user);
            await db.SaveChangesAsync(ct);
        }

        // 2. Отправляем кнопку Mini App
        // ВАЖНО: Замени URL на свой, когда задеплоишь фронтенд (пока можно оставить заглушку)
        var webAppUrl = "https://recipe-app-beta-self.vercel.app/"; 

        var keyboard = new InlineKeyboardMarkup(
            InlineKeyboardButton.WithWebApp("Открыть книгу рецептов 📖", new WebAppInfo { Url = webAppUrl })
        );

        await _botClient.SendMessage(
            chatId: message.Chat.Id,
            text: $"Привет, {user.Username}! Готов создавать шедевры? Нажми кнопку ниже, чтобы управлять своими рецептами.",
            replyMarkup: keyboard,
            cancellationToken: ct
        );
    }

    public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken ct)
    {
        Console.WriteLine("Ошибка API: " + exception.Message);
        return Task.CompletedTask;
    }
}