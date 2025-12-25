using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace RecipeApp.Model;

/// <summary>
/// Рецепт.
/// </summary>
public class Recipe
{
  /// <summary>
  /// ИД рецепта.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Название рецепта.
  /// </summary>
  [Required(ErrorMessage = "Название обязательно")]
  public string Title { get; set; } = string.Empty;

  /// <summary>
  /// Содержимое рецепта.
  /// </summary>
  public string Content { get; set; } = string.Empty;

  /// <summary>
  /// Содержимое рецепта в HTML.
  /// </summary>
  [NotMapped]
  public string ContentHtml => Markdig.Markdown.ToHtml(this.Content);

  /// <summary>
  /// Путь к изображению рецепта.
  /// </summary>
  public string? ImagePath { get; set; }

  /// <summary>
  /// Ингредиенты рецепта.
  /// </summary>
  [SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Должно быть с set, чтобы EF смог обработать.")]
  [SuppressMessage("Usage", "CA1002:Do not expose generic lists", Justification = "Пока оставим как есть.")]
  public List<Ingredient> Ingredients { get; set; } = [];
}