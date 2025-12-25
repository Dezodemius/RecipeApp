using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// Утилиты для работы с типами измерений.
/// </summary>
public static class UnitTypeUtils
{
  /// <summary>
  /// Все типы измерений.
  /// </summary>
  [SuppressMessage("Usage", "CA1002:Do not expose generic lists", Justification = "Пока оставим как есть.")]
  public static readonly List<UnitType> UnitTypes = Enum.GetValues(typeof(UnitType)).Cast<UnitType>().ToList();

  /// <summary>
  /// Получить отображаемое имя для типа измерения.
  /// </summary>
  /// <param name="unit">Тип измерения.</param>
  /// <exception cref="ArgumentOutOfRangeException">Возникает если указанного типа не существует.</exception>
  /// <returns>Отображаемое имя для типа измерения.</returns>
  public static string GetUnitLabel(UnitType unit) => unit switch
  {
    UnitType.Gram => "г",
    UnitType.Piece => "шт",
    UnitType.Tablespoon => "ст. ложка",
    UnitType.Milliliter => "мл",
    UnitType.Liter => "л",
    _ => throw new ArgumentOutOfRangeException(nameof(unit))
  };
}