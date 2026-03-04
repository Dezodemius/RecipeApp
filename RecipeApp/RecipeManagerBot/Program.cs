using Microsoft.EntityFrameworkCore;
using RecipeManagerBot.Data;
using RecipeManagerBot.Services;
using Telegram.Bot;

namespace RecipeManagerBot;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. БД (SQLite)
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=recipes.db"));

        // 2. Регистрация Клиента Телеграм
        builder.Services.AddSingleton<ITelegramBotClient>(sp => 
            new TelegramBotClient(builder.Configuration["BotConfiguration:BotToken"]!));

        // 3. Наши сервисы
        builder.Services.AddSingleton<UpdateHandler>();
        builder.Services.AddHostedService<ReceiverService>();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowVercel", policy =>
            {
                policy.AllowAnyOrigin() // Позже заменишь на конкретный URL от Vercel
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        builder.Services.AddControllers();

        var app = builder.Build();

        app.UseCors("AllowVercel");
        // Авто-создание БД при запуске (удобно для разработки)
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        }
        
        app.UseStaticFiles(); // Чтобы картинки были доступны по ссылке
        app.MapControllers();
        app.Run();
    }
}