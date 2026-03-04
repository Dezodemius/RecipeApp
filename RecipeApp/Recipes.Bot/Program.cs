using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;
using Microsoft.Extensions.Options;

namespace Recipes.Bot;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services
      .AddOptions<TelegramOptions>()
      .Bind(builder.Configuration.GetSection("Telegram"))
      .ValidateOnStart();

    builder.Services.AddSingleton<ITelegramBotClient>(sp =>
    {
      var options = sp.GetRequiredService<IOptions<TelegramOptions>>().Value;
      return new TelegramBotClient(options.BotToken);
    });

    builder.Services.AddGrpcClient<Recipe.Recipe.RecipeClient>(o =>
    {
      o.Address = new Uri(
      builder.Configuration["Grpc:RecipeService"]
      ?? "http://localhost:5001");
    });

    builder.Services.AddControllers();
    builder.Services.AddHostedService<WebhookRegistrar>();

    var app = builder.Build();
    app.MapControllers();
    app.Run();
  }
}