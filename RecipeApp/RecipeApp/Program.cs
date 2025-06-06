using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Components;
using RecipeApp.Services;

namespace RecipeApp;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddRazorComponents()
      .AddInteractiveServerComponents();
    builder.Services.AddDbContext<RecipesDbContext>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    builder.Services.Configure<FormOptions>(options => {
      options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
    });
    builder.Services.AddRazorPages();
    builder.Services.AddServerSideBlazor();
    builder.Services.AddScoped<RecipeService>();

    var app = builder.Build();

    using (var scope = app.Services.CreateScope())
    {
      var db = scope.ServiceProvider.GetRequiredService<RecipesDbContext>();
      db.Database.Migrate();
    }

    if (!app.Environment.IsDevelopment())
    {
      app.UseExceptionHandler("/Error");
      app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.MapRazorComponents<App>()
      .AddInteractiveServerRenderMode();

    app.UseRouting();
    app.UseAntiforgery();

    app.Run();
  }
}