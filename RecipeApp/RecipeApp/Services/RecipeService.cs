using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Сервис для работы с рецептами.
/// </summary>
/// <param name="db">Контекст БД.</param>
public class RecipeService(RecipesDbContext db)
{
  /// <summary>
  /// Получить все рецепты.
  /// </summary>
  /// <returns>Все рецепты.</returns>
  [SuppressMessage("Usage", "CA1002:Do not expose generic lists", Justification = "Пока оставим как есть.")]
  public List<Recipe> GetRecipes()
  {
    return db.Recipes
      .Include(r => r.Ingredients)
      .ToList();
  }

  /// <summary>
  /// Получить рецепт.
  /// </summary>
  /// <param name="id">ИД рецепта.</param>
  /// <returns>Найденный рецепт. Иначе <c>null</c>.</returns>
  public Recipe? GetRecipe(int id)
  {
    return db.Recipes
      .Include(r => r.Ingredients)
      .FirstOrDefault(r => r.Id == id);
  }

  /// <summary>
  /// Сохранить изменения.
  /// </summary>
  public void SaveDbContext()
  {
    db.SaveChanges();
  }

  /// <summary>
  /// Добавить рецепт.
  /// </summary>
  /// <param name="recipe">Рецепт, который нужно добавить.</param>
  public void AddRecipe(Recipe recipe)
  {
    db.Recipes.Add(recipe);
    db.SaveChanges();
  }

  /// <summary>
  /// Удалить рецепт.
  /// </summary>
  /// <param name="id">ИД удаляемого рецепта.</param>
  public void DeleteRecipe(int id)
  {
    var entity = this.GetRecipe(id);
    if (entity != null)
    {
      db.Recipes.Remove(entity);
      db.SaveChanges();
    }
  }
}