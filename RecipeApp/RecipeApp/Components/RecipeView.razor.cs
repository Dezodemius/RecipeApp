using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента для отображения рецепта.
/// </summary>
public partial class RecipeView
{
  #region Поля и свойства

  /// <summary>
  /// Рецепт, который нужно отобразить.
  /// </summary>
  private Recipe? recipe;

  /// <summary>
  /// ИД рецепта.
  /// </summary>
  [Parameter]
  public int Id { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override Task OnInitializedAsync()
  {
    this.recipe = this.RecipeService.GetRecipe(this.Id);

    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  /// <summary>
  /// Отредактировать рецепт.
  /// </summary>
  private void EditRecipe()
  {
    this.NavigationManager.NavigateTo($"/recipes/edit/{this.Id}");
  }

  /// <summary>
  /// Подтвердить удаление.
  /// </summary>
  private async Task ConfirmDelete()
  {
    var confirmed = await this.Js.InvokeAsync<bool>("confirm", "Удалить рецепт?");
    if (confirmed)
    {
      this.RecipeService.DeleteRecipe(this.Id);
      this.NavigationManager.NavigateTo("/");
    }
  }

  /// <summary>
  /// Вернуться назад.
  /// </summary>
  private void GoBack()
  {
    this.NavigationManager.NavigateTo("/");
  }

  #endregion
}