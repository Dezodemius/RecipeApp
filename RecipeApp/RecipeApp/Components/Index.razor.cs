using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecipeApp.Model;

namespace RecipeApp.Components;

/// <summary>
/// Компонента отображения всех рецептов.
/// </summary>
public partial class Index
{
  #region Поля и свойства

  /// <summary>
  /// Количество элементов на одной странице.
  /// </summary>
  private const int PageSize = 10;

  /// <summary>
  /// Рецепты, которые будут отображены на странице.
  /// </summary>
  private List<Recipe>? recipes;

  /// <summary>
  /// Текущая страница.
  /// </summary>
  private int currentPage = 1;

  private string searchText = string.Empty;

  /// <summary>
  /// Текст из поля для поиска.
  /// </summary>
  private string SearchText
  {
    get
    {
      return this.searchText;
    }

    set
    {
      if (this.searchText != value)
      {
        this.searchText = value;
        this.currentPage = 1;
      }
    }
  }

  /// <summary>
  /// Рецепты, которые найдены после поиска.
  /// </summary>
  private IEnumerable<Recipe> FilteredRecipes
  {
    get
    {
      return string.IsNullOrWhiteSpace(this.searchText)
        ? this.recipes!
        : this.recipes!.Where(r =>
          r.Title.Contains(this.searchText, StringComparison.OrdinalIgnoreCase));
    }
  }

  /// <summary>
  /// Общее количество страниц.
  /// </summary>
  private int TotalPages =>
    (int)Math.Ceiling((double)(this.FilteredRecipes?.Count() ?? 0) / PageSize);

  /// <summary>
  /// Рецепты на одной странице.
  /// </summary>
  private IEnumerable<Recipe> PagedRecipes =>
    this.FilteredRecipes
      .Skip((this.currentPage - 1) * PageSize)
      .Take(PageSize)
    ?? [];

  #endregion

  #region Базовый класс

  /// <inheritdoc />
  protected override Task OnInitializedAsync()
  {
    this.recipes = this.RecipeService.GetRecipes();

    return Task.CompletedTask;
  }

  #endregion

  #region Методы

  /// <summary>
  /// Добавить рецепт.
  /// </summary>
  private void AddRecipe()
  {
    this.NavigationManager.NavigateTo("/recipes/create");
  }

  /// <summary>
  /// Открыть рецепт.
  /// </summary>
  /// <param name="id">ИД рецепта, который нужно открыть.</param>
  private void ViewRecipe(int id)
  {
    this.NavigationManager.NavigateTo($"/recipes/{id}");
  }

  /// <summary>
  /// Перейти на указанную страницу.
  /// </summary>
  /// <param name="page">Указанная страница.</param>
  private void GoToPage(int page)
  {
    if (page >= 1 && page <= this.TotalPages)
      this.currentPage = page;
  }

  #endregion
}