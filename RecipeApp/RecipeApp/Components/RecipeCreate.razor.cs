using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для создания рецепта.
/// </summary>
public partial class RecipeCreate
{
  #region Поля и свойства

  private readonly Recipe _recipe = new ()
  {
    Ingredients = []
  };

  #endregion

  #region Методы

  /// <summary>
  /// Сохранить рецепт.
  /// </summary>
  /// <returns>Задача выполнения.</returns>
  private Task Save()
  {
    this.RecipeService.AddRecipe(this._recipe);
    this.NavigationManager.NavigateTo("/");

    return Task.CompletedTask;
  }

  /// <summary>
  /// Отменить создание рецепта.
  /// </summary>
  private void Cancel()
  {
    this.NavigationManager.NavigateTo("/");
  }

  #endregion
}