using System.ComponentModel.DataAnnotations;

namespace RecipeManagerBot.Models;

// Пользователь бота
public class User
{
    [Key]
    public long TelegramId { get; set; }
    public string Username { get; set; } = string.Empty;
    public List<Recipe> MyRecipes { get; set; } = new();
}

// Сам рецепт
public class Recipe
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();
    public List<RecipeStep> Steps { get; set; } = new();
    
    public long OwnerId { get; set; }
    public bool IsPublic { get; set; } = false; // Для шаринга другим
}

// Ингредиент (связан с рецептом)
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty; // Напр: "100г" или "2 шт"
}

// Шаг приготовления
public class RecipeStep
{
    public int Id { get; set; }
    public int StepNumber { get; set; }
    public string Instruction { get; set; } = string.Empty;
    
    // Список URL или FileId картинок для этого шага
    public List<StepImage> Images { get; set; } = new();
}

public class StepImage
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;
}