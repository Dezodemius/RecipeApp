using Microsoft.EntityFrameworkCore;
using RecipeManagerBot.Models;

namespace RecipeManagerBot.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<RecipeStep> Steps => Set<RecipeStep>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Базовая настройка связей, если понадобится кастомизация
        base.OnModelCreating(modelBuilder);
    }
}