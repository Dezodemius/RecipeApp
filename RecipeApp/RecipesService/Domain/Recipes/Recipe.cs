using System;
using System.Collections.Generic;

namespace RecipesService.Domain.Recipes;

/// <summary>
/// Рецепт.
/// </summary>
public class Recipe
{
  #region Поля и свойства

  /// <summary>
  /// ИД рецепта.
  /// </summary>
  public Guid Id { get; private set; }
  
  /// <summary>
  /// Заголовок рецепта.
  /// </summary>
  public string Title { get; private set; }
  
  /// <summary>
  /// Описание рецепта.
  /// </summary>
  public string Description { get; private set; }

  /// <summary>
  /// Шаги приготовления рецепта.
  /// </summary>
  public List<RecipeStep> Steps { get; private set; } = [];
  
  /// <summary>
  /// Ингредиенты, используемые в рецепте.
  /// </summary>
  public List<RecipeIngredient> Ingredients { get; private set; } = [];

  #endregion

  #region Конструкторы

  #region Для EF

  /// <summary>
  /// Конструктор.
  /// </summary>
  private Recipe()
  {
      
  }

  #endregion

  /// <summary>
  /// Конструктор.
  /// </summary>
  /// <param name="id">ИД рецепта.</param>
  /// <param name="title">Заголовок рецепта.</param>
  /// <param name="description">Описание рецепта.</param>
  public Recipe(Guid id, string title, string description)
  {
      Id = id;
      Title = title;
      Description = description;
  }

  #endregion
}
