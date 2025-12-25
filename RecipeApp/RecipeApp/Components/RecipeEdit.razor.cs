using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для редактирования рецепта.
/// </summary>
public partial class RecipeEdit
{
  #region Поля и свойства

  /// <summary>
  /// Рецепт, который будет отредактирован.
  /// </summary>
  private Recipe? _recipe;

  /// <summary>
  /// ИД рецепта, который будет редактирован.
  /// </summary>
  [Parameter]
  public int Id { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override Task OnInitializedAsync()
  {
    this._recipe = this.RecipeService.GetRecipe(this.Id);

    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  /// <summary>
  /// Сохранить изменения в рецепте.
  /// </summary>
  /// <returns>Задача по сохранению рецепта.</returns>
  private Task Save()
  {
    this.RecipeService.SaveDbContext();
    this.NavigationManager.NavigateTo($"/recipes/{this.Id}");

    return Task.CompletedTask;
  }

  /// <summary>
  /// Отменить сохранение рецепта.
  /// </summary>
  private void Cancel()
  {
    this.NavigationManager.NavigateTo($"/recipes/{this.Id}");
  }

  #endregion
}