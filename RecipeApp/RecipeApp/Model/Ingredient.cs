namespace RecipeApp.Model;

/// <summary>
/// Ингредиент.
/// </summary>
public class Ingredient
{
  /// <summary>
  /// ИД ингредиента.
  /// </summary>
  public int Id { get; set; }

  /// <summary>
  /// Имя ингредиента.
  /// </summary>
  public string Name { get; set; } = string.Empty;

  /// <summary>
  /// Количество.
  /// </summary>
  public double Amount { get; set; }

  /// <summary>
  /// Единицы измерения.
  /// </summary>
  public UnitType Unit { get; set; }

  /// <summary>
  /// Количество каллорий.
  /// </summary>
  public float Calories { get; set; }

  /// <summary>
  /// Количество протеина.
  /// </summary>
  public float Protein { get; set; }

  /// <summary>
  /// Количество жира.
  /// </summary>
  public float Fat { get; set; }

  /// <summary>
  /// Количество углеводов.
  /// </summary>
  public float Carbs { get; set; }

  /// <summary>
  /// ИД связанного рецепта.
  /// </summary>
  public int RecipeId { get; set; }

  /// <summary>
  /// Навигационное свойство связанного рецепта.
  /// </summary>
  public Recipe Recipe { get; set; } = null!;
}